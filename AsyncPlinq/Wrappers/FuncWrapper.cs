namespace AsyncPlinq.Wrappers;

internal class FuncWrapper<TInput, TOutput>
{
    private readonly Func<TInput, int, CancellationToken, Task<TOutput>> _method;

    private int _index = -1;

    public FuncWrapper(
        Func<TInput, int, CancellationToken, Task<TOutput>> method)
    {
        _method = method;
    }

    public CancellationTokenSource Cts { get; } = new();

    public Task<TOutput> InvokeAsync(TInput input)
    {
        var currentIndex = Interlocked.Increment(ref _index);
        return _method.Invoke(input, currentIndex, Cts.Token);
    }
}
