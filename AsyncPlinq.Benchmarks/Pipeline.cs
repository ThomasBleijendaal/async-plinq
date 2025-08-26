using System.Threading.Tasks.Dataflow;
using AsyncPlinq;
using BenchmarkDotNet.Attributes;

namespace AsyncPlinq.Benchmarks;

[MemoryDiagnoser]
//[DotNetObjectAllocDiagnoser]
//[DotNetObjectAllocJobConfiguration(1)]
public class Pipeline
{
    [Params(10, 100)]
    public int NumberOfItems { get; set; }

    protected int[] _data = [];

    [GlobalSetup]
    public void Setup()
    {
        _data = [.. Enumerable.Range(0, NumberOfItems)];
    }

    [Benchmark]
    public Task<int[]> LinqPipelineAsync()
    {
        var data = _data
            .Where(x => x % 2 == 0)
            .Select(x => x * 10)
            .ToArray();

        return Task.FromResult(data);
    }

    [Benchmark]
    public async Task<int[]> PlinqPipelineAsync()
    {
        var data1 = await Task.WhenAll(_data
            .Select(async x =>
            {
                await Task.Delay(TimeSpan.FromMilliseconds(1));
                return (x, x % 2 == 0);
            }));

        var data2 = data1.Where(x => x.Item2).Select(x => x.x);

        var data = await Task.WhenAll(data2.Select(async x =>
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1));
            return x * 10;
        }));

        return data;
    }

    [Benchmark]
    public async Task<int[]> AsyncLinqPipelineAsync()
    {
        var data = await _data.ToAsyncEnumerable()
            .Where(async (x, ct) =>
            {
                await Task.Delay(TimeSpan.FromMilliseconds(1));
                return x % 2 == 0;
            })
            .Select(async (x, ix, ct) =>
            {
                await Task.Delay(TimeSpan.FromMilliseconds(1));
                return x * 10;
            })
            .ToArrayAsync();

        return data;
    }

    [Benchmark]
    public async Task<int[]> AsyncPlinqPipelineAsync()
    {
        var data = await _data
            .WhereAsync(async x =>
            {
                await Task.Delay(TimeSpan.FromMilliseconds(1));
                return x % 2 == 0;
            })
            .SelectAsync(async x =>
            {
                await Task.Delay(TimeSpan.FromMilliseconds(1));
                return x * 10;
            })
            .ToArrayAsync();

        return data;
    }

    [Benchmark]
    public async Task<int[]> DataflowPipelineAsync()
    {
        var whereBlock = new TransformManyBlock<int, int>(async x =>
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1));
            return (x % 2 == 0) ? [x] : [];
        }, new() { MaxDegreeOfParallelism = 5 });
        var selectBlock = new TransformBlock<int, int>(async x =>
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1));
            return x * 10;
        }, new() { MaxDegreeOfParallelism = 5 });

        whereBlock.LinkTo(selectBlock, new() { PropagateCompletion = true });

        foreach (var item in _data)
        {
            await whereBlock.SendAsync(item);
        }

        whereBlock.Complete();

        var data = await selectBlock.ReceiveAllAsync().ToArrayAsync();

        return data;
    }
}

