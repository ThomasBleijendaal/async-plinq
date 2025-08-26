using Xunit.Abstractions;

namespace AsyncPlinq.Tests;

[Collection("3")]
public class UnitTest3
{
    private readonly ITestOutputHelper _output;

    public UnitTest3(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task SelectWhereFirst2TestAsync()
    {
        var output = await M.InfiniteItems(_output).SelectAsync(M.AsyncSelector).WhereAsync(M.AsyncPredicate).FirstOrDefaultAsync();

        Assert.True(output % 2 == 0);
    }
}
