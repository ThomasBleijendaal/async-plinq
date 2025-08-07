namespace AsyncPlinq;

public static partial class AsyncPlinqExtensions
{
    extension<TInput>(IAsyncEnumerable<TInput> source)
    {
        public Task WhenAllAsync(CancellationToken token = default)
        {
            return source.ToArrayAsync(token).AsTask();
        }
    }
}
