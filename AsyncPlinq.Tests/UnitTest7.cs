using Xunit.Abstractions;

namespace AsyncPlinq.Tests;

[Collection("7")]
public class UnitTest7
{
    private readonly ITestOutputHelper _output;

    public UnitTest7(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task SelectTest3Async()
    {
        int[] input = [1, 2, 3, 4];

        var output = input.SelectAsync(M.AsyncSelector).SelectAsync(M.AsyncSelector);

        await foreach (var item in output)
        {
            _output.WriteLine(item.ToString());
        }
    }
}
