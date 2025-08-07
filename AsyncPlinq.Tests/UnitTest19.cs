using Xunit.Abstractions;

namespace AsyncPlinq.Tests;

[Collection("19")]
public class UnitTest19
{
    private readonly ITestOutputHelper _output;

    public UnitTest19(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task SelectManyTestAsync()
    {
        int[] input = [1, 2, 3, 4];

        var output = input.SelectManyAsync(M.AsyncProducer);

        await foreach (var item in output)
        {
            _output.WriteLine(item.ToString());
        }
    }
}
