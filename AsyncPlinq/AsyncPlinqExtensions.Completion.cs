namespace AsyncPlinq;

public static partial class AsyncPlinqExtensions
{
    extension<TInput>(IEnumerable<TInput> source)
    {
        public async Task WhenAllAsync(
            Func<TInput, Task> selector,
            int maxDegreeOfParallelism = 5,
            CancellationToken token = default)
        {
            using var semaphore = new SemaphoreSlim(maxDegreeOfParallelism);

            await Task.WhenAll(source.Select(async (x) =>
            {
                try
                {
                    await semaphore.WaitAsync(token);
                    await selector.Invoke(x);
                }
                finally
                {
                    semaphore.Release();
                }
            }));
        }
    }

    extension<TInput>(IAsyncEnumerable<TInput> source)
    {
        public Task WhenAllAsync(CancellationToken token = default)
        {
            return source.ToArrayAsync(token).AsTask();
        }
    }
}
