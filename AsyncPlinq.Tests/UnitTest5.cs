using Xunit.Abstractions;

namespace AsyncPlinq.Tests;

[Collection("5")]
public class UnitTest5
{
    private readonly ITestOutputHelper _output;

    public UnitTest5(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task SelectIndexTestAsync()
    {
        int[] input = [1, 2, 3, 4];

        var output = input.SelectAsync(M.AsyncSelectorIx);

        await foreach (var item in output)
        {
            _output.WriteLine(item.ToString());
        }
    }
}
