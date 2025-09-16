using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using AsyncPlinq;

namespace AsyncAnimator.Simulations;

internal static class Sim10SelectMany
{
    private static ConcurrentBag<Timing> _inputData;
    private static long _simStart;

    public static async Task RunAsync()
    {
        _inputData = new ConcurrentBag<Timing>();

        _simStart = Stopwatch.GetTimestamp();

        await Task.Delay(Sim.Timeout());

        var result1 = await new[] { 1, 4, 7 }
            .SelectAsync(async (i, index, ct) =>
            {
                var start = Stopwatch.GetElapsedTime(_simStart);

                await Task.Delay(Sim.Timeout() * 2);

                var end = Stopwatch.GetElapsedTime(_simStart);

                _inputData.Add(new Timing(i, 1, true, start, end));

                return i;
            }, 2)
            .SelectManyAsync(GetArrayAsync, 4)
            .SelectAsync(async (i, index, ct) =>
            {
                var start = Stopwatch.GetElapsedTime(_simStart);

                await Task.Delay(Sim.Timeout());

                var end = Stopwatch.GetElapsedTime(_simStart);

                _inputData.Add(new Timing(i, 3, true, start, end));

                return i;
            }, 4)
            .WhereAsync(async (i, index, ct) =>
            {
                var start = Stopwatch.GetElapsedTime(_simStart);

                await Task.Delay(Sim.Timeout());

                var end = Stopwatch.GetElapsedTime(_simStart);

                _inputData.Add(new Timing(i, 4, i % 2 == 1, start, end));

                return i % 2 == 1;
            }, 3)
            .ToArrayAsync();

        await Task.Delay(1000);

        Draw.DrawTimings("sim10.gif", _inputData, ["SelectAsync", "SelectManyAsync", "SelectAsync", "WhereAsync"]);
    }

    private static async IAsyncEnumerable<int> GetArrayAsync(int i, int index, [EnumeratorCancellation] CancellationToken ct)
    {
        for (var x = 0; x < 3; x++)
        {
            var start = Stopwatch.GetElapsedTime(_simStart);

            await Task.Delay(100);

            var end = Stopwatch.GetElapsedTime(_simStart);

            _inputData.Add(new Timing(i + x, 2, true, start, end));

            yield return i + x;
        }
    }
}
