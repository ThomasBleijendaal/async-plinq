namespace AsyncPlinq;

public static partial class AsyncPlinqExtensions
{
    extension<TInput>(IEnumerable<TInput> source)
    {
        public async IAsyncEnumerable<TInput> WhereAsync(Func<TInput, Task<bool>> predicate, int? maxDegreeOfParallelism = null)
        {
            using var semaphore = new SemaphoreSlim(maxDegreeOfParallelism ?? AsyncPlinq.DefaultMaxDegreeOfParallelism);

            var results = ExecuteWithSemaphore(source, predicate, maxDegreeOfParallelism);

            foreach (var (input, task) in results)
            {
                if (await task)
                {
                    yield return input;
                }
            }
        }
    }

    extension<TInput>(IEnumerable<Task<TInput>> source)
    {
        public async IAsyncEnumerable<TInput> WhereAsync(Func<TInput, bool> predicate, int? maxDegreeOfParallelism = null)
        {
            using var semaphore = new SemaphoreSlim(maxDegreeOfParallelism ?? AsyncPlinq.DefaultMaxDegreeOfParallelism);

            var results = ExecuteWithSemaphore(source, async (input) =>
            {
                var data = await input;
                return (data, predicate.Invoke(data));
            }, maxDegreeOfParallelism);

            foreach (var (input, task) in results)
            {
                var (result, @bool) = await task;
                if (@bool)
                {
                    yield return result;
                }
            }
        }

        public async IAsyncEnumerable<TInput> WhereAsync(Func<TInput, Task<bool>> predicate, int? maxDegreeOfParallelism = null)
        {
            using var semaphore = new SemaphoreSlim(maxDegreeOfParallelism ?? AsyncPlinq.DefaultMaxDegreeOfParallelism);

            var results = ExecuteWithSemaphore(source, async (input) =>
            {
                var data = await input;
                return (data, await predicate.Invoke(data));
            }, maxDegreeOfParallelism);

            foreach (var (input, task) in results)
            {
                var (result, @bool) = await task;
                if (@bool)
                {
                    yield return result;
                }
            }
        }
    }

    extension<TInput>(IAsyncEnumerable<TInput> source)
    {
        // TODO
        //public async Task<IEnumerable<TInput>> WhereAsync(Func<TInput, bool> predicate, int? maxDegreeOfParallelism = null)
        //{
        //    using var semaphore = new SemaphoreSlim(maxDegreeOfParallelism ?? AsyncPlinq.DefaultMaxDegreeOfParallelism);

        //    var results = await ExecuteWithSemaphoreAsync(source, predicate, maxDegreeOfParallelism);

        //    return results.Where(Snd).Select(Fst);
        //}

        public async Task<IEnumerable<TInput>> WhereAsync(Func<TInput, Task<bool>> predicate, int? maxDegreeOfParallelism = null)
        {
            using var semaphore = new SemaphoreSlim(maxDegreeOfParallelism ?? AsyncPlinq.DefaultMaxDegreeOfParallelism);

            var results = await ExecuteWithSemaphoreAsync(source, predicate, maxDegreeOfParallelism);

            return results.Where(Snd).Select(Fst);
        }
    }
}
