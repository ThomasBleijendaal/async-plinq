using System.Collections.Concurrent;
using System.Diagnostics;
using AsyncPlinq;

namespace AsyncAnimator.Simulations;

internal static class Sim07AsyncPlinq
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

                await Task.Delay(Sim.Timeout() / 2);

                var end = Stopwatch.GetElapsedTime(simStart);

                inputData.Add(new Timing(i, 1, true, start, end));

                return i;
            }, 2)
            .WhereAsync(async (i, index, ct) =>
            {
                var start = Stopwatch.GetElapsedTime(simStart);

                await Task.Delay(Sim.Timeout() * 3);

                var end = Stopwatch.GetElapsedTime(simStart);

                inputData.Add(new Timing(i, 2, index % 2 == 1, start, end));

                return index % 2 == 1;
            }, 5)
            .SelectAsync(async i =>
            {
                var start = Stopwatch.GetElapsedTime(simStart);

                await Task.Delay(Sim.Timeout() / 2);

                var end = Stopwatch.GetElapsedTime(simStart);

                inputData.Add(new Timing(i, 3, true, start, end));

                return i;
            }, 1)
            .WhereAsync(async (i, index, ct) =>
            {
                var start = Stopwatch.GetElapsedTime(simStart);

                await Task.Delay(Sim.Timeout() * 4);

                var end = Stopwatch.GetElapsedTime(simStart);

                inputData.Add(new Timing(i, 4, index % 2 == 1, start, end));

                return index % 2 == 1;
            }, 10)
            .ToArrayAsync();

        await Task.Delay(1000);

        Draw.DrawTimings("sim7.gif", inputData, ["SelectAsync", "WhereAsync", "SelectAsync", "WhereAsync"]);
    }
}
