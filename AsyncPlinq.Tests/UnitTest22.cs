using Xunit.Abstractions;

namespace AsyncPlinq.Tests;

[Collection("22")]
public class UnitTest22
{
    private readonly ITestOutputHelper _output;

    public UnitTest22(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task SelectWhereWhenAllTestAsync()
    {
        int[] input = [1, 2, 3, 4];

        await input.SelectAsync(M.AsyncSelector).WhereAsync(M.AsyncPredicate).WhenAllAsync();
    }
}
