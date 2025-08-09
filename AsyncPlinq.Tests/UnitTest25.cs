using System.Collections.Concurrent;
using Xunit.Abstractions;

namespace AsyncPlinq.Tests;

[Collection("25")]
public class UnitTest25
{
    private readonly ITestOutputHelper _output;

    public UnitTest25(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task CancellationTestAsync()
    {
        int[] input = [1, 2, 3, 4];

        var canceled = new ConcurrentBag<int>();

        var output = await input.SelectAsync(async (i, index, ct) =>
        {
            try
            {
                await Task.Delay(i * 1000, ct);
            }
            catch
            {
                canceled.Add(i);
            }

            return i;
        }).FirstOrDefaultAsync();

        await Task.Delay(1000);

        Assert.True(canceled.Order().SequenceEqual([2, 3, 4]));
    }
}
