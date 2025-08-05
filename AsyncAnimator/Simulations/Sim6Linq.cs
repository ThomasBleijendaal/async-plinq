using System.Diagnostics;

namespace AsyncAnimator.Simulations;

internal static class Sim6Linq
{
    public static void Run()
    {
        var inputData = new List<Timing>();

        var simStart = Stopwatch.GetTimestamp();

        Thread.Sleep(Sim.Timeout());

        var result = Enumerable.Range(1, 10)
            .Select(i =>
            {
                var start = Stopwatch.GetElapsedTime(simStart);

                Thread.Sleep(Sim.Timeout() / 2);

                var end = Stopwatch.GetElapsedTime(simStart);

                inputData.Add(new Timing(i, 1, start, end));

                return i;
            })
            .Where(i =>
            {
                var start = Stopwatch.GetElapsedTime(simStart);

                Thread.Sleep(Sim.Timeout() * 3);

                var end = Stopwatch.GetElapsedTime(simStart);

                inputData.Add(new Timing(i, 2, start, end));

                return true;
            })
            .Select(i =>
            {
                var start = Stopwatch.GetElapsedTime(simStart);

                Thread.Sleep(Sim.Timeout() / 2);

                var end = Stopwatch.GetElapsedTime(simStart);

                inputData.Add(new Timing(i, 3, start, end));

                return i;
            })
            .Where(i =>
            {
                var start = Stopwatch.GetElapsedTime(simStart);

                Thread.Sleep(Sim.Timeout() * 4);

                var end = Stopwatch.GetElapsedTime(simStart);

                inputData.Add(new Timing(i, 4, start, end));

                return true;
            })
            .ToArray();

        Draw.DrawTimings("sim6.gif", inputData);
    }
}
