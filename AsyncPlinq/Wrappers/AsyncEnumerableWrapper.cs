namespace AsyncPlinq.Wrappers;

internal class AsyncEnumerableWrapper<TInput, TOutput>
{
    private readonly Func<TInput, int, CancellationToken, IAsyncEnumerable<TOutput>> _method;

    private int _index = -1;

    public AsyncEnumerableWrapper(
        Func<TInput, int, CancellationToken, IAsyncEnumerable<TOutput>> method)
    {
        _method = method;
    }

    public CancellationTokenSource Cts { get; } = new();

    public IAsyncEnumerable<TOutput> InvokeAsync(TInput input)
    {
        var currentIndex = Interlocked.Increment(ref _index);
        return _method.Invoke(input, currentIndex, Cts.Token);
    }
}
