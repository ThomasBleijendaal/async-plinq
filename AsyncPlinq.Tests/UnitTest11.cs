using Xunit.Abstractions;

namespace AsyncPlinq.Tests;

[Collection("11")]
public class UnitTest11
{
    private readonly ITestOutputHelper _output;

    public UnitTest11(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task WhereTest3Async()
    {
        int[] input = [1, 2, 3, 4];

        var output = input.WhereAsync(M.AsyncPredicate).WhereAsync(M.SyncPredicate);

        await foreach (var item in output)
        {
            _output.WriteLine(item.ToString());
        }
    }
}
