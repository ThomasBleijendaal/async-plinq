using Xunit.Abstractions;

namespace AsyncPlinq.Tests;

[Collection("27")]
public class UnitTest27
{
    private readonly ITestOutputHelper _output;

    public UnitTest27(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task ExceptionAsync()
    {
        int[] input = [1, 2, 3, 4];

        var enumerable = input.SelectAsync((i, index, ct) =>
        {
            if (index == 2)
            {
                return Task.FromException<int>(new Exception());
            }

            return Task.FromResult(index);
        }, 2);

        await Assert.ThrowsAsync<Exception>(async () => await enumerable.ToArrayAsync());
    }
}
