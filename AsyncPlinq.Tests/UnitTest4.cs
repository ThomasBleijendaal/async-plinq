using Xunit.Abstractions;

namespace AsyncPlinq.Tests;

[Collection("4")]
public class UnitTest4
{
    private readonly ITestOutputHelper _output;

    public UnitTest4(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task SelectExceptionTestAsync()
    {
        int[] input = [1, 2, 3, 4];

        var enumerable = input.SelectAsync(M.AsyncSelector);

        await enumerable.ToArrayAsync();
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await enumerable.ToArrayAsync());
    }
}
