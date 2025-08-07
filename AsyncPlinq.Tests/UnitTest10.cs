using Xunit.Abstractions;

namespace AsyncPlinq.Tests;

[Collection("10")]
public class UnitTest10
{
    private readonly ITestOutputHelper _output;

    public UnitTest10(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task WhereTest2Async()
    {
        int[] input = [1, 2, 3, 4];

        var output = input.WhereAsync(M.AsyncPredicate).WhereAsync(M.AsyncPredicate);

        await foreach (var item in output)
        {
            _output.WriteLine(item.ToString());
        }
    }
}
