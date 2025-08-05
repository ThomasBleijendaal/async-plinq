using System.Collections.Concurrent;
using System.Diagnostics;
using AsyncPlinq;

namespace AsyncAnimator.Simulations;

internal static class Sim5AsyncPlinq
{
    public static async Task RunAsync()
    {
        var inputData = new ConcurrentBag<Timing>();

        var simStart = Stopwatch.GetTimestamp();

        await Task.Delay(Sim.Timeout());

        var result1 = await Enumerable.Range(1, 10)
            .SelectAsync(async i =>
            {
                var start = Stopwatch.GetElapsedTime(simStart);

                await Task.Delay(Sim.Timeout());

                var end = Stopwatch.GetElapsedTime(simStart);

                inputData.Add(new Timing(i, 1, start, end));

                return i;
            })
            .WhereAsync(async i =>
            {
                var start = Stopwatch.GetElapsedTime(simStart);

                await Task.Delay(Sim.Timeout());

                var end = Stopwatch.GetElapsedTime(simStart);

                inputData.Add(new Timing(i, 2, start, end));

                return true;
            }, 3)
            .ToArrayAsync();

        await Task.Delay(1000);

        Draw.DrawTimings("sim5.gif", inputData);
    }
}
