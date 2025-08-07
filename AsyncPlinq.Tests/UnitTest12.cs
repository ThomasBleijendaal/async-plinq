using Xunit.Abstractions;

namespace AsyncPlinq.Tests;

[Collection("12")]
public class UnitTest12
{
    private readonly ITestOutputHelper _output;

    public UnitTest12(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task SelectWhereTestAsync()
    {
        int[] input = [1, 2, 3, 4];

        var output = input.SelectAsync(M.AsyncSelector).WhereAsync(M.AsyncPredicate);

        await foreach (var item in output)
        {
            _output.WriteLine(item.ToString());
        }
    }
}
