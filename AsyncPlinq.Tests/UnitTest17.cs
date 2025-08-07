using Xunit.Abstractions;

namespace AsyncPlinq.Tests;

[Collection("17")]
public class UnitTest17
{
    private readonly ITestOutputHelper _output;

    public UnitTest17(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task SelectWhereFirstTestAsync()
    {
        int[] input = [1, 2, 3, 4];

        var output = await input.SelectAsync(M.AsyncSelector).WhereAsync(M.AsyncPredicate).FirstOrDefaultAsync();

        Assert.True(output == 2);
    }
}
