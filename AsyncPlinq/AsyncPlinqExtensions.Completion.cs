namespace AsyncPlinq;

public static partial class AsyncPlinqExtensions
{
    extension<TInput>(IEnumerable<TInput> source)
    {
        public async Task<TResult[]> ToArrayAsync<TResult>(Func<TInput, Task<TResult>> selector, int? maxDegreeOfParallelism = null)
        {
            using var semaphore = new SemaphoreSlim(maxDegreeOfParallelism ?? AsyncPlinq.DefaultMaxDegreeOfParallelism);

            var results = ExecuteWithSemaphore(source, selector, maxDegreeOfParallelism);

            return await Task.WhenAll(results.Select(Snd));
        }

        public async Task WhenAllAsync<TResult>(Func<TInput, Task<TResult>> selector, int? maxDegreeOfParallelism = null)
        {
            await source.ToArrayAsync(selector, maxDegreeOfParallelism);
        }

        public async Task<List<TResult>> ToListAsync<TResult>(Func<TInput, Task<TResult>> selector, int? maxDegreeOfParallelism = null)
        {
            return [.. await source.ToArrayAsync(selector, maxDegreeOfParallelism)];
        }

        public async Task<Dictionary<TKey, TValue>> ToDictionaryAsync<TKey, TValue>(Func<TInput, Task<(TKey, TValue)>> selector, int? maxDegreeOfParallelism = null)
            where TKey : notnull
        {
            return (await source.ToArrayAsync(selector, maxDegreeOfParallelism)).ToDictionary();
        }
    }

    extension<TInput>(IEnumerable<Task<TInput>> source)
    {
        public async Task<TResult[]> ToArrayAsync<TResult>(Func<TInput, TResult> selector, int? maxDegreeOfParallelism = null)
        {
            using var semaphore = new SemaphoreSlim(maxDegreeOfParallelism ?? AsyncPlinq.DefaultMaxDegreeOfParallelism);

            var results = ExecuteWithSemaphore(source, async (input) =>
            {
                var data = await input;
                return selector.Invoke(data);
            }, maxDegreeOfParallelism);

            return await Task.WhenAll(results.Select(Snd));
        }

        public async Task WhenAllAsync<TResult>(Func<TInput, TResult> selector, int? maxDegreeOfParallelism = null)
        {
            await source.ToArrayAsync(selector, maxDegreeOfParallelism);
        }

        public async Task<List<TResult>> ToListAsync<TResult>(Func<TInput, TResult> selector, int? maxDegreeOfParallelism = null)
        {
            return [.. await source.ToArrayAsync(selector, maxDegreeOfParallelism)];
        }

        public async Task<Dictionary<TKey, TValue>> ToDictionaryAsync<TKey, TValue>(Func<TInput, (TKey, TValue)> selector, int? maxDegreeOfParallelism = null)
            where TKey : notnull
        {
            return (await source.ToArrayAsync(selector, maxDegreeOfParallelism)).ToDictionary();
        }

        public async Task<TResult[]> ToArrayAsync<TResult>(Func<TInput, Task<TResult>> selector, int? maxDegreeOfParallelism = null)
        {
            using var semaphore = new SemaphoreSlim(maxDegreeOfParallelism ?? AsyncPlinq.DefaultMaxDegreeOfParallelism);

            var results = ExecuteWithSemaphore(source, async (input) =>
            {
                var data = await input;
                return await selector.Invoke(data);
            }, maxDegreeOfParallelism);

            return await Task.WhenAll(results.Select(Snd));
        }

        public async Task WhenAllAsync<TResult>(Func<TInput, Task<TResult>> selector, int? maxDegreeOfParallelism = null)
        {
            await source.ToArrayAsync(selector, maxDegreeOfParallelism);
        }

        public async Task<List<TResult>> ToListAsync<TResult>(Func<TInput, Task<TResult>> selector, int? maxDegreeOfParallelism = null)
        {
            return [.. await source.ToArrayAsync(selector, maxDegreeOfParallelism)];
        }

        public async Task<Dictionary<TKey, TValue>> ToDictionaryAsync<TKey, TValue>(Func<TInput, Task<(TKey, TValue)>> selector, int? maxDegreeOfParallelism = null)
            where TKey : notnull
        {
            return (await source.ToArrayAsync(selector, maxDegreeOfParallelism)).ToDictionary();
        }
    }

    extension<TInput>(IAsyncEnumerable<TInput> source)
    {
        // TODO: 
        // public async Task<TResult[]> ToArrayAsync<TResult>(Func<TInput, TResult> selector, int? maxDegreeOfParallelism = null)

        public async Task<TResult[]> ToArrayAsync<TResult>(Func<TInput, Task<TResult>> selector, int? maxDegreeOfParallelism = null)
        {
            using var semaphore = new SemaphoreSlim(maxDegreeOfParallelism ?? AsyncPlinq.DefaultMaxDegreeOfParallelism);

            var results = await ExecuteWithSemaphoreAsync(source, selector, maxDegreeOfParallelism);

            return [.. results.Select(Snd)];
        }

        public async Task WhenAllAsync<TResult>(Func<TInput, Task<TResult>> selector, int? maxDegreeOfParallelism = null)
        {
            await source.ToArrayAsync(selector, maxDegreeOfParallelism);
        }

        public async Task<List<TResult>> ToListAsync<TResult>(Func<TInput, Task<TResult>> selector, int? maxDegreeOfParallelism = null)
        {
            return [.. await source.ToArrayAsync(selector, maxDegreeOfParallelism)];
        }

        public async Task<Dictionary<TKey, TValue>> ToDictionaryAsync<TKey, TValue>(Func<TInput, Task<(TKey, TValue)>> selector, int? maxDegreeOfParallelism = null)
            where TKey : notnull
        {
            return (await source.ToArrayAsync(selector, maxDegreeOfParallelism)).ToDictionary();
        }
    }
}
