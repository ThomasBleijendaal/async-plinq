namespace AsyncPlinq;

public static partial class AsyncPlinqExtensions
{
    extension<TInput>(IEnumerable<TInput> source)
    {
        /// <summary>
        /// Runs the given selector in parallel.
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="maxDegreeOfParallelism"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task WhenAllAsync(
            Func<TInput, Task> selector,
            int maxDegreeOfParallelism = 5,
            CancellationToken token = default)
        {
            using var semaphore = new SemaphoreSlim(maxDegreeOfParallelism);

            await Task.WhenAll(source.Select(async (x) =>
            {
                await semaphore.WaitAsync(token);
                try
                {
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
        /// <summary>
        /// Completes the given enumerable.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task WhenAllAsync(CancellationToken token = default) => source.ToArrayAsync(token).AsTask();
    }
}
