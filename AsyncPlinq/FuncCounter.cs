namespace AsyncPlinq;

internal class FuncCounter<TInput, TOutput>
{
    private readonly Func<TInput, int, Task<TOutput>> _method;
    private int _index = -1;

    public FuncCounter(Func<TInput, int, Task<TOutput>> method)
    {
        _method = method;
    }

    public async Task<TOutput> InvokeAsync(TInput input)
    {
        var currentIndex = Interlocked.Increment(ref _index);
        return await _method.Invoke(input, currentIndex).ConfigureAwait(false);
    }
}
