using System.Threading.Tasks.Dataflow;

namespace AsyncPlinq;

internal static class BlockBuilder
{
    public static Block<TInput, TOutput> Create<TInput, TOutput>(
        Func<TInput, Task<TOutput>> selector,
        int? maxDegreeOfParallelism = null)
    {
        var transform = new TransformBlock<TInput, TOutput>(selector, new()
        {
            MaxDegreeOfParallelism = maxDegreeOfParallelism ?? AsyncPlinq.DefaultMaxDegreeOfParallelism,
            BoundedCapacity = AsyncPlinq.BoundedCapacity(maxDegreeOfParallelism)
        });

        return new(transform, transform);
    }

    public static Block<TInput, TOutput> Create<TInput, TOutput>(
        Func<TInput, int, Task<TOutput>> selector,
        int? maxDegreeOfParallelism = null)
    {
        var funcCounter = new FuncCounter<TInput, TOutput>(selector);

        return Create<TInput, TOutput>(funcCounter.InvokeAsync, maxDegreeOfParallelism);
    }

    public static Block<TInput, TInput> Create<TInput>(
        Func<TInput, Task<bool>> predicate,
        int? maxDegreeOfParallelism = null)
    {
        var transform = new TransformBlock<TInput, (bool, TInput)>(
            async (input) =>
            {
                var result = await predicate.Invoke(input);
                return (result, input);
            },
            new()
            {
                MaxDegreeOfParallelism = maxDegreeOfParallelism ?? AsyncPlinq.DefaultMaxDegreeOfParallelism,
                BoundedCapacity = AsyncPlinq.BoundedCapacity(maxDegreeOfParallelism)
            });

        var transform2 = new TransformBlock<(bool, TInput), TInput>(AsyncPlinqExtensions.Snd);

        var noAction = new ActionBlock<(bool, TInput)>((_) => { });

        transform.LinkTo(transform2, BlockOptions.DefaultOptions, AsyncPlinqExtensions.Fst);
        transform.LinkTo(noAction, BlockOptions.DefaultOptions, AsyncPlinqExtensions.NotFst);

        return new(transform, transform2);
    }

    public static Block<TInput, TInput> Create<TInput>(
        Func<TInput, int, Task<bool>> predicate,
        int? maxDegreeOfParallelism = null)
    {
        var funcCounter = new FuncCounter<TInput, bool>(predicate);

        return Create<TInput>(funcCounter.InvokeAsync, maxDegreeOfParallelism);
    }
}
