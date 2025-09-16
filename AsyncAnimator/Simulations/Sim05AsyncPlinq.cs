using System.Collections.Concurrent;
using System.Diagnostics;
using AsyncPlinq;

namespace AsyncAnimator.Simulations;

internal static class Sim05AsyncPlinq
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

                inputData.Add(new Timing(i, 1, true, start, end));

                return i;
            }, 2)
            .WhereAsync(async i =>
            {
                var start = Stopwatch.GetElapsedTime(simStart);

                await Task.Delay(Sim.Timeout() * 10);

                var end = Stopwatch.GetElapsedTime(simStart);

                inputData.Add(new Timing(i, 2, i % 2 == 1, start, end));

                return i % 2 == 1;
            }, 5)
            .ToArrayAsync();

        await Task.Delay(1000);

        Draw.DrawTimings("sim5.gif", inputData, ["SelectAsync", "WhereAsync"]);
    }
}
