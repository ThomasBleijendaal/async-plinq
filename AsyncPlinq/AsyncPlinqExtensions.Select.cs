namespace AsyncPlinq;

public static partial class AsyncPlinqExtensions
{
    extension<TInput>(IEnumerable<TInput> source)
    {
        public IEnumerable<Task<TResult>> SelectAsync<TResult>(Func<TInput, Task<TResult>> selector, int? maxDegreeOfParallelism = null)
        {
            using var semaphore = new SemaphoreSlim(maxDegreeOfParallelism ?? AsyncPlinq.DefaultMaxDegreeOfParallelism);

            var results = ExecuteWithSemaphore(source, selector, maxDegreeOfParallelism);

            return results.Select(Snd);
        }
    }

    extension<TInput>(IEnumerable<Task<TInput>> source)
    {
        public IEnumerable<Task<TResult>> SelectAsync<TResult>(Func<TInput, TResult> selector, int? maxDegreeOfParallelism = null)
        {
            using var semaphore = new SemaphoreSlim(maxDegreeOfParallelism ?? AsyncPlinq.DefaultMaxDegreeOfParallelism);

            var results = ExecuteWithSemaphore(source, async (input) =>
            {
                var data = await input;
                return selector.Invoke(data);
            }, maxDegreeOfParallelism);

            return results.Select(Snd);
        }

        public IEnumerable<Task<TResult>> SelectAsync<TResult>(Func<TInput, Task<TResult>> selector, int? maxDegreeOfParallelism = null)
        {
            using var semaphore = new SemaphoreSlim(maxDegreeOfParallelism ?? AsyncPlinq.DefaultMaxDegreeOfParallelism);

            var results = ExecuteWithSemaphore(source, async (input) =>
            {
                var data = await input;
                return await selector.Invoke(data);
            }, maxDegreeOfParallelism);

            return results.Select(Snd);
        }
    }

    extension<TInput>(IAsyncEnumerable<TInput> source)
    {
        // TODO
        //public async Task<IEnumerable<TResult>> SelectAsync<TResult>(Func<TInput, TResult> selector, int? maxDegreeOfParallelism = null)
        //{
        //    using var semaphore = new SemaphoreSlim(maxDegreeOfParallelism ?? AsyncPlinq.DefaultMaxDegreeOfParallelism);

        //    var results = await ExecuteWithSemaphoreAsync(source, selector, maxDegreeOfParallelism);

        //    return results.Select(Snd);
        //}

        public async Task<IEnumerable<TResult>> SelectAsync<TResult>(Func<TInput, Task<TResult>> selector, int? maxDegreeOfParallelism = null)
        {
            using var semaphore = new SemaphoreSlim(maxDegreeOfParallelism ?? AsyncPlinq.DefaultMaxDegreeOfParallelism);

            var results = await ExecuteWithSemaphoreAsync(source, selector, maxDegreeOfParallelism);

            return results.Select(Snd);
        }
    }
}
