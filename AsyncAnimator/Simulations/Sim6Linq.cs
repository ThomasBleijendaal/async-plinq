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

                inputData.Add(new Timing(i, 1, true, start, end));

                return i;
            })
            .Where((i, index) =>
            {
                var start = Stopwatch.GetElapsedTime(simStart);

                Thread.Sleep(Sim.Timeout() * 3);

                var end = Stopwatch.GetElapsedTime(simStart);

                inputData.Add(new Timing(i, 2, index % 2 == 1, start, end));

                return index % 2 == 1;
            })
            .Select(i =>
            {
                var start = Stopwatch.GetElapsedTime(simStart);

                Thread.Sleep(Sim.Timeout() / 2);

                var end = Stopwatch.GetElapsedTime(simStart);

                inputData.Add(new Timing(i, 3, true, start, end));

                return i;
            })
            .Where((i, index) =>
            {
                var start = Stopwatch.GetElapsedTime(simStart);

                Thread.Sleep(Sim.Timeout() * 4);

                var end = Stopwatch.GetElapsedTime(simStart);

                inputData.Add(new Timing(i, 4, index % 2 == 1, start, end));

                return index % 2 == 1;
            })
            .ToArray();

        Draw.DrawTimings("sim6.gif", inputData);
    }
}
