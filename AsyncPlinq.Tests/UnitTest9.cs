using Xunit.Abstractions;

namespace AsyncPlinq.Tests;

[Collection("9")]
public class UnitTest9
{
    private readonly ITestOutputHelper _output;

    public UnitTest9(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task WhereTestAsync()
    {
        int[] input = [1, 2, 3, 4];

        var output = input.WhereAsync(M.AsyncPredicate);

        await foreach (var item in output)
        {
            _output.WriteLine(item.ToString());
        }
    }
}
