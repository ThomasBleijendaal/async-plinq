using System.Collections.Concurrent;
using System.Diagnostics;

namespace AsyncAnimator.Simulations;

internal static class Sim1Linq
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

                inputData.Add(new Timing(i, 1, start, end));

                return i;
            })
            .Where(i =>
            {
                var start = Stopwatch.GetElapsedTime(simStart);

                Thread.Sleep(Sim.Timeout());

                var end = Stopwatch.GetElapsedTime(simStart);

                inputData.Add(new Timing(i, 2, start, end));

                return true;
            })
            .ToArray();

        Draw.DrawTimings("sim1.gif", inputData);
    }
}
