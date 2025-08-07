namespace AsyncPlinq;

internal class FuncCounter<TInput, TOutput>
{
    private readonly Func<TInput, int, CancellationToken, Task<TOutput>> _method;
    private int _index = -1;

    public FuncCounter(Func<TInput, int, CancellationToken, Task<TOutput>> method)
    {
        _method = method;
    }

    public Task<TOutput> InvokeAsync(TInput input)
    {
        var currentIndex = Interlocked.Increment(ref _index);
        return _method.Invoke(input, currentIndex);
    }
}
