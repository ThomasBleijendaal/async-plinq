using Xunit.Abstractions;

namespace AsyncPlinq.Tests;

[Collection("15")]
public class UnitTest15
{
    private readonly ITestOutputHelper _output;

    public UnitTest15(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task SelectWhereTest2AsyncLinqAsync()
    {
        int[] input = [1, 2, 3, 4];

        var output = input.ToAsyncEnumerable().Select(M.AsyncSelectorCt).Where(M.AsyncPredicateCt);

        await foreach (var item in output)
        {
            _output.WriteLine(item.ToString());
        }
    }
}
