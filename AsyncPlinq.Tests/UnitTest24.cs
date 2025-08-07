using Xunit.Abstractions;

namespace AsyncPlinq.Tests;

[Collection("24")]
public class UnitTest24
{
    private readonly ITestOutputHelper _output;

    public UnitTest24(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task SelectManyTestAsync()
    {
        int[] input = [1, 2, 3, 4];

        var output = input
            .SelectManyAsync(M.AsyncProducerIxAsync);

        await foreach (var item in output)
        {
            _output.WriteLine(item.ToString());
        }
    }
}
