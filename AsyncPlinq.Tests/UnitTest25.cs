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

        var canceledTasks = new ConcurrentBag<int>();

        var output = input.SelectAsync(async (i, index, token) =>
        {
            try
            {
                await Task.Delay(index * 1000, token);
            }
            catch
            {
                canceledTasks.Add(index);
            }

            return i;

        }, 10);

        await foreach (var item in output)
        {
            _output.WriteLine(item.ToString());
        }

        Assert.True(canceledTasks.OrderBy(x => x).SequenceEqual([1, 2, 3]));
    }
}
