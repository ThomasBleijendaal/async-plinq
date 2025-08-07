using Xunit.Abstractions;

namespace AsyncPlinq.Tests;

public static class M
{
    public static readonly Func<int, int> SyncSelector =
        i => { return i + 1; };

    public static readonly Func<int, Task<int>> AsyncSelector =
        async i => { await Task.Delay(1000); return i + 1; };

    public static readonly Func<int, int, Task<int>> AsyncSelectorIx =
        async (i, index) => { await Task.Delay(1000); return ((index + 1) * 1000) + i + 1; };

    public static readonly Func<int, Task<IEnumerable<int>>> AsyncProducer =
        async i => { await Task.Delay(1000); return Enumerable.Range((i + 1) * 100, 10); };

    public static readonly Func<int, int, Task<IEnumerable<int>>> AsyncProducerIx =
        async (i, index) => { await Task.Delay(1000); return Enumerable.Range(((index + 1) * 1000) + (i + 1) * 100, 10); };

    public static readonly Func<int, CancellationToken, ValueTask<int>> AsyncSelectorCt =
        async (i, ct) => { await Task.Delay(1000); return i + 1; };

    public static readonly Func<int, bool> SyncPredicate =
        i => { return i % 2 == 0; };

    public static readonly Func<int, Task<bool>> AsyncPredicate =
        async i => { await Task.Delay(1000); return i % 2 == 0; };

    public static readonly Func<int, int, Task<bool>> AsyncPredicateIx =
        async (i, index) => { await Task.Delay(1000); return (((i + 1) * 1000) + index) % 2 == 0; };

    public static readonly Func<int, CancellationToken, ValueTask<bool>> AsyncPredicateCt =
        async (i, ct) => { await Task.Delay(1000); return i % 2 == 0; };

    public static IEnumerable<int> InfiniteItems(ITestOutputHelper output)
    {
        var i = 0;

        do
        {
            output.WriteLine(i.ToString());
            yield return ++i;
        }
        while (true);
    }
}
