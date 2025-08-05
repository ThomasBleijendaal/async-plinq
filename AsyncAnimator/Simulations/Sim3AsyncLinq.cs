using System.Collections.Concurrent;
using System.Diagnostics;

namespace AsyncAnimator.Simulations;

internal static class Sim3AsyncLinq
{
    public static async Task RunAsync()
    {
        var inputData = new ConcurrentBag<Timing>();

        var simStart = Stopwatch.GetTimestamp();

        await Task.Delay(Sim.Timeout());

        var result = await Enumerable.Range(1, 10)
            .ToAsyncEnumerable()
            .Select(async (i, index, ct) =>
            {
                var start = Stopwatch.GetElapsedTime(simStart);

                await Task.Delay(Sim.Timeout());

                var end = Stopwatch.GetElapsedTime(simStart);

                inputData.Add(new Timing(i, 1, start, end));

                return i;
            })
            .Where(async (i, index, ct) =>
            {
                var start = Stopwatch.GetElapsedTime(simStart);

                await Task.Delay(Sim.Timeout());

                var end = Stopwatch.GetElapsedTime(simStart);

                inputData.Add(new Timing(i, 2, start, end));

                return true;
            })
            .ToArrayAsync();

        await Task.Delay(1000);

        Draw.DrawTimings("sim3.gif", inputData);
    }
}
