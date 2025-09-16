using System.Collections.Concurrent;
using System.Diagnostics;

namespace AsyncAnimator.Simulations;

internal static class Sim02Plinq
{
    public static async Task RunAsync()
    {
        var inputData = new ConcurrentBag<Timing>();

        var simStart = Stopwatch.GetTimestamp();

        await Task.Delay(Sim.Timeout());

        var tasks1 = Enumerable.Range(1, 10).AsParallel().WithDegreeOfParallelism(3)
            .Select(async i =>
            {
                var start = Stopwatch.GetElapsedTime(simStart);

                await Task.Delay(Sim.Timeout());

                var end = Stopwatch.GetElapsedTime(simStart);

                inputData.Add(new Timing(i, 1, true, start, end));

                return i;
            });

        var result1 = await Task.WhenAll(tasks1);

        var tasks2 = result1.AsParallel().WithDegreeOfParallelism(3)
            .Select(async i =>
            {
                var start = Stopwatch.GetElapsedTime(simStart);

                await Task.Delay(Sim.Timeout());

                var end = Stopwatch.GetElapsedTime(simStart);

                inputData.Add(new Timing(i, 2, i % 2 == 1, start, end));

                return (i, i % 2 == 1);
            });

        var result2 = await Task.WhenAll(tasks2);

        var result = result2
            .Where(x => x.Item2)
            .Select(x => x.i)
            .ToArray();

        await Task.Delay(1000);

        Draw.DrawTimings("sim2b.gif", inputData, ["Select", "Where"]);
    }
}
