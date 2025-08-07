using Xunit.Abstractions;

namespace AsyncPlinq.Tests;

[Collection("20")]
public class UnitTest20
{
    private readonly ITestOutputHelper _output;

    public UnitTest20(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task SelectManyTestAsync()
    {
        int[] input = [1, 2, 3, 4];

        var output = input.SelectManyAsync(M.AsyncProducerIx);

        await foreach (var item in output)
        {
            _output.WriteLine(item.ToString());
        }
    }
}
