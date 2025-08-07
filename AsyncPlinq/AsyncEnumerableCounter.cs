namespace AsyncPlinq;

internal class AsyncEnumerableCounter<TInput, TOutput>
{
    private readonly Func<TInput, int, IAsyncEnumerable<TOutput>> _method;
    private int _index = -1;

    public AsyncEnumerableCounter(Func<TInput, int, IAsyncEnumerable<TOutput>> method)
    {
        _method = method;
    }

    public IAsyncEnumerable<TOutput> InvokeAsync(TInput input)
    {
        var currentIndex = Interlocked.Increment(ref _index);
        return _method.Invoke(input, currentIndex);
    }
}
