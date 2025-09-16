using System.Collections.Concurrent;
using System.Diagnostics;

namespace AsyncAnimator.Simulations;

internal static class Sim01Linq
{
    public static void Run()
    {
        var inputData = new ConcurrentBag<Timing>();

        var simStart = Stopwatch.GetTimestamp();

        Thread.Sleep(Sim.Timeout());

        var result = Enumerable.Range(1, 10)
            .Select(i =>
            {
                var start = Stopwatch.GetElapsedTime(simStart);

                Thread.Sleep(Sim.Timeout());

                var end = Stopwatch.GetElapsedTime(simStart);

                inputData.Add(new Timing(i, 1, true, start, end));

                return i;
            })
            .Where(i =>
            {
                var start = Stopwatch.GetElapsedTime(simStart);

                Thread.Sleep(Sim.Timeout());

                var end = Stopwatch.GetElapsedTime(simStart);

                inputData.Add(new Timing(i, 2, i % 2 == 1, start, end));

                return i % 2 == 1;
            })
            .ToArray();

        Draw.DrawTimings("sim1.gif", inputData, ["Select", "Where"]);
    }
}
