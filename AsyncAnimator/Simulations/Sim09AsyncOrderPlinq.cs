using System.Collections.Concurrent;
using System.Diagnostics;
using AsyncPlinq;

namespace AsyncAnimator.Simulations;

internal static class Sim09AsyncOrderPlinq
{
    public static async Task RunAsync()
    {
        var inputData = new ConcurrentBag<Timing>();

        var simStart = Stopwatch.GetTimestamp();

        await Task.Delay(Sim.Timeout());

        var result1 = await Enumerable.Range(1, 10)
            .SelectAsync(async (i, index, ct) =>
            {
                var start = Stopwatch.GetElapsedTime(simStart);

                await Task.Delay(Sim.Timeout() * (10 - i));

                var end = Stopwatch.GetElapsedTime(simStart);

                inputData.Add(new Timing(i, 1, true, start, end));

                return i;
            }, 10)
            .WhereAsync(async (i, index, ct) =>
            {
                var start = Stopwatch.GetElapsedTime(simStart);

                await Task.Delay(Sim.Timeout() * (10 - i));

                var end = Stopwatch.GetElapsedTime(simStart);

                inputData.Add(new Timing(i, 2, index % 2 == 1, start, end));

                return index % 2 == 1;
            }, 10)
            .SelectAsync(async (i, index, ct) =>
            {
                var start = Stopwatch.GetElapsedTime(simStart);

                await Task.Delay(Sim.Timeout() * (10 - i));

                var end = Stopwatch.GetElapsedTime(simStart);

                inputData.Add(new Timing(i, 3, true, start, end));

                return i;
            }, 10)
            .WhereAsync(async (i, index, ct) =>
            {
                var start = Stopwatch.GetElapsedTime(simStart);

                await Task.Delay(Sim.Timeout() * (10 - i));

                var end = Stopwatch.GetElapsedTime(simStart);

                inputData.Add(new Timing(i, 4, index % 2 == 1, start, end));

                return index % 2 == 1;
            }, 10)
            .ToArrayAsync();

        await Task.Delay(1000);

        Draw.DrawTimings("sim9.gif", inputData, ["SelectAsync", "WhereAsync", "SelectAsync", "WhereAsync"]);
    }
}
