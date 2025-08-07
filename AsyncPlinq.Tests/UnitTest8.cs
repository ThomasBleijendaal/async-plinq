using Xunit.Abstractions;

namespace AsyncPlinq.Tests;

[Collection("8")]
public class UnitTest8
{
    private readonly ITestOutputHelper _output;

    public UnitTest8(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task SelectTest4Async()
    {
        int[] input = [1, 2, 3, 4];

        var output = input.SelectAsync(M.AsyncSelector).SelectAsync(M.SyncSelector);

        await foreach (var item in output)
        {
            _output.WriteLine(item.ToString());
        }
    }
}
