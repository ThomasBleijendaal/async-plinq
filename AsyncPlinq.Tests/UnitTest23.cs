using Xunit.Abstractions;

namespace AsyncPlinq.Tests;

[Collection("23")]
public class UnitTest23
{
    private readonly ITestOutputHelper _output;

    public UnitTest23(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task SelectManyTestAsync()
    {
        int[] input = [1, 2, 3, 4];

        var output = input
            .SelectManyAsync(M.AsyncProducerAsync);

        await foreach (var item in output)
        {
            _output.WriteLine(item.ToString());
        }
    }
}
