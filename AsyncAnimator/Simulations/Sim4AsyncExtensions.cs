using System.Collections.Concurrent;
using System.Diagnostics;

namespace AsyncAnimator.Simulations;

internal static class Sim4AsyncExtensions
{
    public static async Task RunAsync()
    {
        var inputData = new ConcurrentBag<Timing>();

        var simStart = Stopwatch.GetTimestamp();

        await Task.Delay(Sim.Timeout());

        var result1 = await Enumerable.Range(1, 10)
            .ToListAsync(async i =>
            {
                var start = Stopwatch.GetElapsedTime(simStart);

                await Task.Delay(Sim.Timeout());

                var end = Stopwatch.GetElapsedTime(simStart);

                inputData.Add(new Timing(i, 1, start, end));

                return i;
            });

        var result2 = await result1
            .ToListAsync(async i =>
            {
                var start = Stopwatch.GetElapsedTime(simStart);

                await Task.Delay(Sim.Timeout());

                var end = Stopwatch.GetElapsedTime(simStart);

                inputData.Add(new Timing(i, 2, start, end));

                return true;
            }, 3);

        await Task.Delay(1000);

        Draw.DrawTimings("sim4.gif", inputData);
    }

    public static async Task<TResult[]> ToListAsync<TSource, TResult>(
        this IEnumerable<TSource> source,
        Func<TSource, Task<TResult>> selector,
        int maxDegreeOfParallelism = 5)
    {
        using var semaphore = new SemaphoreSlim(maxDegreeOfParallelism);

        var aggregateTask = Task.WhenAll(source.Select(async (x) =>
        {
            try
            {
                await semaphore.WaitAsync();
                return await selector.Invoke(x);
            }
            finally
            {
                semaphore.Release();
            }
        }));

        return await aggregateTask;
    }
}
