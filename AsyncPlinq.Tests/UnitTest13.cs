using Xunit.Abstractions;

namespace AsyncPlinq.Tests;

[Collection("13")]
public class UnitTest13
{
    private readonly ITestOutputHelper _output;

    public UnitTest13(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task SelectWhereIndexTestAsync()
    {
        int[] input = [1, 2, 3, 4];

        var output = input.SelectAsync(M.AsyncSelectorIx).WhereAsync(M.AsyncPredicateIx);

        await foreach (var item in output)
        {
            _output.WriteLine(item.ToString());
        }
    }
}
