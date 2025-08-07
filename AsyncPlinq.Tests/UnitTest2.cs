using Xunit.Abstractions;

namespace AsyncPlinq.Tests;

[Collection("2")]
public class UnitTest2
{
    private readonly ITestOutputHelper _output;

    public UnitTest2(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task SelectTestAsync()
    {
        int[] input = [1, 2, 3, 4];

        var output = input.SelectAsync(M.AsyncSelector);

        await foreach (var item in output)
        {
            _output.WriteLine(item.ToString());
        }
    }
}
