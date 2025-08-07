using System.Threading.Tasks.Dataflow;

namespace AsyncPlinq;

internal static class BlockBuilder
{
    public static Block<TInput, TOutput> Create<TInput, TOutput>(
        Func<TInput, Task<TOutput>> selector,
        int? maxDegreeOfParallelism = null)
    {
        var transform = new TransformBlock<TInput, TOutput>(selector, CreateOptions(maxDegreeOfParallelism));
        return new(transform, transform);
    }

    public static Block<TInput, TOutput> Create<TInput, TOutput>(
        Func<TInput, int, CancellationToken, Task<TOutput>> selector,
        int? maxDegreeOfParallelism = null)
    {
        var cts = new CancellationTokenSource();
        var funcCounter = new FuncCounter<TInput, TOutput>(selector);
        return Create<TInput, TOutput>(funcCounter.InvokeAsync, maxDegreeOfParallelism);
    }

    public static Block<TInput, TOutput> Create<TInput, TOutput>(
        Func<TInput, Task<IEnumerable<TOutput>>> selector,
        int? maxDegreeOfParallelism = null)
    {
        var transform = new TransformManyBlock<TInput, TOutput>(selector, CreateOptions(maxDegreeOfParallelism));
        return new(transform, transform);
    }

    public static Block<TInput, TOutput> Create<TInput, TOutput>(
        Func<TInput, int, Task<IEnumerable<TOutput>>> selector,
        int? maxDegreeOfParallelism = null)
    {
        var funcCounter = new FuncCounter<TInput, IEnumerable<TOutput>>(selector);
        return Create<TInput, TOutput>(funcCounter.InvokeAsync, maxDegreeOfParallelism);
    }

    public static Block<TInput, TOutput> Create<TInput, TOutput>(
        Func<TInput, IAsyncEnumerable<TOutput>> selector,
        int? maxDegreeOfParallelism = null)
    {
        var transform = new TransformManyBlock<TInput, TOutput>(selector, CreateOptions(maxDegreeOfParallelism));
        return new(transform, transform);
    }

    public static Block<TInput, TOutput> Create<TInput, TOutput>(
        Func<TInput, int, IAsyncEnumerable<TOutput>> selector,
        int? maxDegreeOfParallelism = null)
    {
        var funcCounter = new AsyncEnumerableCounter<TInput, TOutput>(selector);
        return Create<TInput, TOutput>(funcCounter.InvokeAsync, maxDegreeOfParallelism);
    }

    public static Block<TInput, TInput> Create<TInput>(
        Func<TInput, Task<bool>> predicate,
        int? maxDegreeOfParallelism = null)
    {
        var transform = new TransformManyBlock<TInput, TInput>(
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

        return new(transform, transform);
    }

    public static Block<TInput, TInput> Create<TInput>(
        Func<TInput, int, Task<bool>> predicate,
        int? maxDegreeOfParallelism = null)
    {
        var funcCounter = new FuncCounter<TInput, bool>(predicate);
        return Create<TInput>(funcCounter.InvokeAsync, maxDegreeOfParallelism);
    }

    private static ExecutionDataflowBlockOptions CreateOptions(int? maxDegreeOfParallelism)
        => new()
        {
            MaxDegreeOfParallelism = maxDegreeOfParallelism ?? AsyncPlinq.DefaultMaxDegreeOfParallelism,
            BoundedCapacity = AsyncPlinq.BoundedCapacity(maxDegreeOfParallelism)
        };
}
