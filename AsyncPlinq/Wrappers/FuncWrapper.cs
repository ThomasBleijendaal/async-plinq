namespace AsyncPlinq.Wrappers;

internal class FuncWrapper<TInput, TResult>
{
    private readonly Func<TInput, int, CancellationToken, TResult> _method;

    private int _index = -1;

    public FuncWrapper(
        Func<TInput, int, CancellationToken, TResult> method)
    {
        _method = method;
    }

    public CancellationTokenSource Cts { get; } = new();

    public TResult InvokeAsync(TInput input)
    {
        var currentIndex = Interlocked.Increment(ref _index);
        return _method.Invoke(input, currentIndex, Cts.Token);
    }
}
