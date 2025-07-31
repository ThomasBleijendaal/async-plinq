using Xunit.Abstractions;

namespace AsyncPlinq.Tests;

public class UnitTest1
{
    private readonly ITestOutputHelper _output;

    private readonly Func<int, int> _syncSelector = i => { return i + 1; };
    private readonly Func<int, Task<int>> _asyncSelector = async i => { await Task.Delay(1000); return i + 1; };
    private readonly Func<int, bool> _syncPredicate = i => { return i % 2 == 0; };
    private readonly Func<int, Task<bool>> _asyncPredicate = async i => { await Task.Delay(1000); return i % 2 == 0; };

    public UnitTest1(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task SelectTestAsync()
    {
        int[] input = [1, 2, 3, 4];

        var output = input.SelectAsync(_asyncSelector);

        await foreach (var item in output)
        {
            _output.WriteLine(item.ToString());
        }
    }

    [Fact]
    public async Task SelectTest2Async()
    {
        int[] input = [1, 2, 3, 4];

        var output = input.SelectAsync(_asyncSelector, maxDegreeOfParallelism: 2);

        await foreach (var item in output)
        {
            _output.WriteLine(item.ToString());
        }
    }

    [Fact]
    public async Task SelectTest3Async()
    {
        int[] input = [1, 2, 3, 4];

        var output = input.SelectAsync(_asyncSelector).SelectAsync(_asyncSelector);

        await foreach (var item in output)
        {
            _output.WriteLine(item.ToString());
        }
    }

    [Fact]
    public async Task SelectTest4Async()
    {
        int[] input = [1, 2, 3, 4];

        var output = input.SelectAsync(_asyncSelector).SelectAsync(_syncSelector);

        await foreach (var item in output)
        {
            _output.WriteLine(item.ToString());
        }
    }

    [Fact]
    public async Task WhereTestAsync()
    {
        int[] input = [1, 2, 3, 4];

        var output = input.WhereAsync(_asyncPredicate);

        await foreach (var item in output)
        {
            _output.WriteLine(item.ToString());
        }
    }

    [Fact]
    public async Task WhereTest2Async()
    {
        int[] input = [1, 2, 3, 4];

        var output = input.WhereAsync(_asyncPredicate).WhereAsync(_asyncPredicate);

        await foreach (var item in output)
        {
            _output.WriteLine(item.ToString());
        }
    }

    [Fact]
    public async Task WhereTest3Async()
    {
        int[] input = [1, 2, 3, 4];

        var output = input.WhereAsync(_asyncPredicate).WhereAsync(_syncPredicate);

        await foreach (var item in output)
        {
            _output.WriteLine(item.ToString());
        }
    }

    [Fact]
    public async Task SelectWhereTestAsync()
    {
        int[] input = [1, 2, 3, 4];

        var output = input.SelectAsync(_asyncSelector).WhereAsync(_asyncPredicate);

        await foreach (var item in output)
        {
            _output.WriteLine(item.ToString());
        }
    }

    [Fact]
    public async Task SelectWhereToArrayTestAsync()
    {
        int[] input = [1, 2, 3, 4];

        var output = await input.SelectAsync(_asyncSelector).WhereAsync(_asyncPredicate).ToArrayAsync();

        Assert.True(output.Length == 2);

        foreach (var item in output)
        {
            _output.WriteLine(item.ToString());
        }
    }
}
