using Xunit.Abstractions;

namespace AsyncPlinq.Tests;

[Collection("6")]
public class UnitTest6
{
    private readonly ITestOutputHelper _output;

    public UnitTest6(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task SelectTest2Async()
    {
        int[] input = [1, 2, 3, 4];

        var output = input.SelectAsync(M.AsyncSelector, maxDegreeOfParallelism: 2);

        await foreach (var item in output)
        {
            _output.WriteLine(item.ToString());
        }
    }
}
