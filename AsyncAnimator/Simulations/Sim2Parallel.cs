using System.Collections.Concurrent;
using System.Diagnostics;

namespace AsyncAnimator.Simulations;

internal static class Sim2Linq
{
    public static async Task RunAsync()
    {
        var inputData = new ConcurrentBag<Timing>();

        var simStart = Stopwatch.GetTimestamp();

        await Task.Delay(Sim.Timeout());

        var result1 = await Task.WhenAll(Enumerable.Range(1, 10)
            .Select(async i =>
            {
                var start = Stopwatch.GetElapsedTime(simStart);

                await Task.Delay(Sim.Timeout());

                var end = Stopwatch.GetElapsedTime(simStart);

                inputData.Add(new Timing(i, 1, start, end));

                return i;
            }));


        var result2 = await Task.WhenAll(result1
            .Select(async i =>
            {
                var start = Stopwatch.GetElapsedTime(simStart);

                await Task.Delay(Sim.Timeout());

                var end = Stopwatch.GetElapsedTime(simStart);

                inputData.Add(new Timing(i, 2, start, end));

                return true;
            }));

        var result = result2.Where(x => x).ToArray();

        await Task.Delay(1000);

        Draw.DrawTimings("sim2.gif", inputData);
    }
}
