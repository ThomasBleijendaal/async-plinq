using Xunit.Abstractions;

namespace AsyncPlinq.Tests;

[Collection("21")]
public class UnitTest21
{
    private readonly ITestOutputHelper _output;

    public UnitTest21(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task SelectManySelectManyTestAsync()
    {
        int[] input = [1, 2, 3, 4];

        var output = input.SelectManyAsync(M.AsyncProducer, 8).SelectManyAsync(M.AsyncProducer, 30);

        await foreach (var item in output)
        {
            _output.WriteLine(item.ToString());
        }
    }
}
