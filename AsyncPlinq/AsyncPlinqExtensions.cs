namespace AsyncPlinq;

public static partial class AsyncPlinqExtensions
{
    private static IEnumerable<(TInput, Task<TResult>)> ExecuteWithSemaphore<TInput, TResult>(
        IEnumerable<TInput> source,
        Func<TInput, Task<TResult>> action,
        int? maxDegreeOfParallelism)
    {
        using var semaphore = new SemaphoreSlim(maxDegreeOfParallelism ?? AsyncPlinq.DefaultMaxDegreeOfParallelism);

        return source.Select((x) =>
        {
            return (x, Task.Run(async () =>
            {
                try
                {
                    await semaphore.WaitAsync();
                    return await action.Invoke(x);
                }
                finally
                {
                    semaphore.Release();
                }
            }));
        });
    }

    private static async Task<IEnumerable<(TInput, TResult)>> ExecuteWithSemaphoreAsync<TInput, TResult>(
        IAsyncEnumerable<TInput> source,
        Func<TInput, Task<TResult>> action,
        int? maxDegreeOfParallelism)
    {
        using var semaphore = new SemaphoreSlim(maxDegreeOfParallelism ?? AsyncPlinq.DefaultMaxDegreeOfParallelism);

        await using var enumerator = source.GetAsyncEnumerator();

        var tasks = new List<(TInput, Task<TResult>)>();

        while (await InvokeItemAsync(() => enumerator.MoveNextAsync()))
        {
            tasks.Add((enumerator.Current, InvokeItemAsync(() => new ValueTask<TResult>(action.Invoke(enumerator.Current)))));
        }

        await Task.WhenAll(tasks.Select(Snd));

        return tasks.Select(x => (x.Item1, x.Item2.Result));

        async Task<TData> InvokeItemAsync<TData>(Func<ValueTask<TData>> provider)
        {
            try
            {
                await semaphore.WaitAsync();
                return await provider.Invoke();
            }
            finally
            {
                semaphore.Release();
            }
        }
    }

    private static T1 Fst<T1, T2>((T1 t1, T2 t2) input) => input.t1;
    private static T2 Snd<T1, T2>((T1 t1, T2 t2) input) => input.t2;
}
