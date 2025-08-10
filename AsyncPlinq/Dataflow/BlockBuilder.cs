using System.Threading.Tasks.Dataflow;
using AsyncPlinq;
using AsyncPlinq.Wrappers;

namespace AsyncPlinq.Dataflow;

internal static class BlockBuilder
{
    public static Block<TInput, TOutput> Create<TInput, TOutput>(
        Func<TInput, Task<TOutput>> selector,
        int? maxDegreeOfParallelism = null)
    {
        var transform = CreateSelect(selector, maxDegreeOfParallelism);
        return new(transform, transform);
    }

    public static Block<TInput, TOutput> Create<TInput, TOutput>(
        Func<TInput, int, CancellationToken, Task<TOutput>> selector,
        int? maxDegreeOfParallelism = null)
    {
        var wrapper = new FuncWrapper<TInput, TOutput>(selector);
        var transform = CreateSelect<TInput, TOutput>(wrapper.InvokeAsync, maxDegreeOfParallelism);
        return new(transform, transform, wrapper.Cts);
    }

    private static TransformBlock<TInput, TOutput> CreateSelect<TInput, TOutput>(
        Func<TInput, Task<TOutput>> selector,
        int? maxDegreeOfParallelism)
        => new(selector, CreateOptions(maxDegreeOfParallelism));

    public static Block<TInput, TOutput> Create<TInput, TOutput>(
        Func<TInput, Task<IEnumerable<TOutput>>> selector,
        int? maxDegreeOfParallelism = null)
    {
        var transform = CreateSelectMany(selector, maxDegreeOfParallelism);
        return new(transform, transform);
    }

    public static Block<TInput, TOutput> Create<TInput, TOutput>(
        Func<TInput, int, CancellationToken, Task<IEnumerable<TOutput>>> selector,
        int? maxDegreeOfParallelism = null)
    {
        var wrapper = new FuncWrapper<TInput, IEnumerable<TOutput>>(selector);
        var transform = CreateSelectMany<TInput, TOutput>(wrapper.InvokeAsync, maxDegreeOfParallelism);
        return new(transform, transform, wrapper.Cts);
    }

    private static TransformManyBlock<TInput, TOutput> CreateSelectMany<TInput, TOutput>(
        Func<TInput, Task<IEnumerable<TOutput>>> selector,
        int? maxDegreeOfParallelism)
        => new TransformManyBlock<TInput, TOutput>(selector, CreateOptions(maxDegreeOfParallelism));

    public static Block<TInput, TOutput> Create<TInput, TOutput>(
        Func<TInput, IAsyncEnumerable<TOutput>> selector,
        int? maxDegreeOfParallelism = null)
    {
        var transform = CreateSelectMany(selector, maxDegreeOfParallelism);
        return new(transform, transform);
    }

    public static Block<TInput, TOutput> Create<TInput, TOutput>(
        Func<TInput, int, CancellationToken, IAsyncEnumerable<TOutput>> selector,
        int? maxDegreeOfParallelism = null)
    {
        var wrapper = new AsyncEnumerableWrapper<TInput, TOutput>(selector);
        var transform = CreateSelectMany<TInput, TOutput>(wrapper.InvokeAsync, maxDegreeOfParallelism);
        return new(transform, transform, wrapper.Cts);
    }

    private static TransformManyBlock<TInput, TOutput> CreateSelectMany<TInput, TOutput>(
        Func<TInput, IAsyncEnumerable<TOutput>> selector,
        int? maxDegreeOfParallelism)
        => new(selector, CreateOptions(maxDegreeOfParallelism));

    public static Block<TInput, TInput> Create<TInput>(
        Func<TInput, Task<bool>> predicate,
        int? maxDegreeOfParallelism = null)
    {
        var transform = CreateWhereBlock(predicate, maxDegreeOfParallelism);
        return new(transform, transform);
    }

    public static Block<TInput, TInput> Create<TInput>(
        Func<TInput, int, CancellationToken, Task<bool>> predicate,
        int? maxDegreeOfParallelism = null)
    {
        var wrapper = new FuncWrapper<TInput, bool>(predicate);
        var transform = CreateWhereBlock<TInput>(wrapper.InvokeAsync, maxDegreeOfParallelism);
        return new(transform, transform, wrapper.Cts);
    }

    private static TransformManyBlock<TInput, TInput> CreateWhereBlock<TInput>(
        Func<TInput, Task<bool>> predicate,
        int? maxDegreeOfParallelism)
        => new(
            async (input) =>
            {
                var result = await predicate.Invoke(input).ConfigureAwait(false);
                if (result)
                {
                    return [input];
                }
                else
                {
                    return [];
                }
            },
            CreateOptions(maxDegreeOfParallelism));

    private static ExecutionDataflowBlockOptions CreateOptions(int? maxDegreeOfParallelism)
        => new()
        {
            MaxDegreeOfParallelism = maxDegreeOfParallelism ?? AsyncPlinq.DefaultMaxDegreeOfParallelism,
            BoundedCapacity = AsyncPlinq.BoundedCapacity(maxDegreeOfParallelism),
            // this can be true because it's inherently a linear pipe that can only be executed once
            SingleProducerConstrained = true
        };
}
