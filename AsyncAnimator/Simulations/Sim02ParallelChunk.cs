using System.Collections.Concurrent;
using System.Diagnostics;

namespace AsyncAnimator.Simulations;

internal static class Sim02LinqChunk
{
    public static async Task RunAsync()
    {
        var inputData = new ConcurrentBag<Timing>();

        var simStart = Stopwatch.GetTimestamp();

        await Task.Delay(Sim.Timeout());

        var tasks1 = Enumerable.Range(1, 10)
            .Select(async i =>
            {
                var start = Stopwatch.GetElapsedTime(simStart);

                await Task.Delay(Sim.Timeout());

                var end = Stopwatch.GetElapsedTime(simStart);

                inputData.Add(new Timing(i, 1, true, start, end));

                return i;
            })
            .Chunk(3);

        var result1 = new List<int>();

        foreach (var chunk in tasks1)
        {
            result1.AddRange(await Task.WhenAll(chunk));
        }

        var tasks2 = result1
            .Select(async i =>
            {
                var start = Stopwatch.GetElapsedTime(simStart);

                await Task.Delay(Sim.Timeout());

                var end = Stopwatch.GetElapsedTime(simStart);

                inputData.Add(new Timing(i, 2, i % 2 == 1, start, end));

                return (i, i % 2 == 1);
            })
            .Chunk(3);

        var result2 = new List<(int i, bool)>();

        foreach (var chunk in tasks2)
        {
            result2.AddRange(await Task.WhenAll(chunk));
        }

        var result = result2
            .Where(x => x.Item2)
            .Select(x => x.i)
            .ToArray();

        await Task.Delay(1000);

        Draw.DrawTimings("sim2a.gif", inputData, ["Select", "Where"]);
    }
}
