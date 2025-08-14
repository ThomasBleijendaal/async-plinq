using Xunit.Abstractions;

namespace AsyncPlinq.Tests;

[Collection("26")]
public class UnitTest26
{
    private readonly ITestOutputHelper _output;

    public UnitTest26(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task SelectWhereWhenAllTestAsync()
    {
        int[] input = [1, 2, 3, 4];

        await input.WhenAllAsync(M.AsyncSelector);
    }
}
