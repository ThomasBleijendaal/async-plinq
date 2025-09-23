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
            int? maxDegreeOfParallelism = null,
            CancellationToken token = default)
        {
            using var semaphore = new SemaphoreSlim(maxDegreeOfParallelism ?? AsyncPlinq.DefaultMaxDegreeOfParallelism);

            await Task.WhenAll(source.Select(async (x) =>
            {
                await semaphore.WaitAsync(token).ConfigureAwait(false);
                try
                {
                    await selector.Invoke(x).ConfigureAwait(false);
                }
                finally
                {
                    semaphore.Release();
                }
            })).ConfigureAwait(false);
        }

        /// <summary>
        /// Runs the given selector in parallel and returns the resulting array.
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="maxDegreeOfParallelism"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<TResult[]> ToArrayAsync<TResult>(
            Func<TInput, Task<TResult>> selector,
            int? maxDegreeOfParallelism = null,
            CancellationToken token = default)
        {
            using var semaphore = new SemaphoreSlim(maxDegreeOfParallelism ?? AsyncPlinq.DefaultMaxDegreeOfParallelism);

            return await Task.WhenAll(source.Select(async (x) =>
            {
                await semaphore.WaitAsync(token).ConfigureAwait(false);
                try
                {
                    return await selector.Invoke(x).ConfigureAwait(false);
                }
                finally
                {
                    semaphore.Release();
                }
            })).ConfigureAwait(false);
        }

        /// <summary>
        /// Runs the given selector in parallel and returns the resulting array.
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="maxDegreeOfParallelism"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<TResult>> ToListAsync<TResult>(
            Func<TInput, Task<TResult>> selector,
            int? maxDegreeOfParallelism,
            CancellationToken token = default)
        => [.. (await source.ToArrayAsync(selector, maxDegreeOfParallelism, token).ConfigureAwait(false))];
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
