using Xunit.Abstractions;

namespace AsyncPlinq.Tests;

[Collection("16")]
public class UnitTest16
{
    private readonly ITestOutputHelper _output;

    public UnitTest16(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task SelectWhereToArrayTestAsync()
    {
        int[] input = [1, 2, 3, 4];

        var output = await input.SelectAsync(M.AsyncSelector).WhereAsync(M.AsyncPredicate).ToArrayAsync();

        Assert.True(output.Length == 2);

        foreach (var item in output)
        {
            _output.WriteLine(item.ToString());
        }
    }
}
