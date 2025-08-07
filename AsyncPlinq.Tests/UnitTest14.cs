using Xunit.Abstractions;

namespace AsyncPlinq.Tests;

[Collection("14")]
public class UnitTest14
{
    private readonly ITestOutputHelper _output;

    public UnitTest14(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task SelectWhereTest2Async()
    {
        int[] input = [1, 2, 3, 4, 5, 6, 7, 8];

        var output = input.SelectAsync(M.AsyncSelector, maxDegreeOfParallelism: 2).WhereAsync(M.AsyncPredicate, maxDegreeOfParallelism: 2);

        await foreach (var item in output)
        {
            _output.WriteLine(item.ToString());
        }
    }
}
