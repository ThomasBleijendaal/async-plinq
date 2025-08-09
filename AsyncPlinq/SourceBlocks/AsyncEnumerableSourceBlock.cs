using System.Threading.Tasks.Dataflow;
using AsyncPlinq;
using AsyncPlinq.Dataflow;

namespace AsyncPlinq.SourceBlocks;

internal class AsyncEnumerableSourceBlock<T> : ISourceBlock<T>, IUpstreamBlock
{
    private readonly IAsyncEnumerable<T> _source;

    private readonly TaskCompletionSource _tcs;
    private CancellationTokenSource? _cts;

    private ITargetBlock<T>? _target;

    public AsyncEnumerableSourceBlock(IAsyncEnumerable<T> source)
    {
        _source = source;

        _tcs = new();
        Completion = _tcs.Task;
    }

    public Task Completion { get; private set; }

    public void Complete() => Complete(default);

    public void Complete(CancellationToken token)
    {
        lock (_tcs)
        {
            if (Completion == _tcs.Task)
            {
                Completion = ReadEnumerableAsync(token);
            }
        }
    }

    public ValueTask DisposeAsync()
    {
        _cts?.Cancel();

        return ValueTask.CompletedTask;
    }

    public T? ConsumeMessage(DataflowMessageHeader messageHeader, ITargetBlock<T> target, out bool messageConsumed)
    {
        messageConsumed = false;
        return default;
    }

    public void Fault(Exception exception)
    {
        _tcs.SetException(exception);
    }

    public IDisposable LinkTo(ITargetBlock<T> target, DataflowLinkOptions linkOptions)
    {
        _target = target;

        return DummyDisposable.Dummy;
    }

    public void ReleaseReservation(DataflowMessageHeader messageHeader, ITargetBlock<T> target)
    {
    }

    public bool ReserveMessage(DataflowMessageHeader messageHeader, ITargetBlock<T> target)
    {
        return false;
    }

    private async Task ReadEnumerableAsync(CancellationToken token)
    {
        if (_target == null)
        {
            throw new InvalidOperationException("No target");
        }

        if (token != default)
        {
            _cts ??= CancellationTokenSource.CreateLinkedTokenSource(token);
        }

        var ct = _cts?.Token ?? default;

        try
        {
            await foreach (var item in _source.WithCancellation(ct).ConfigureAwait(false))
            {
                await _target.SendAsync(item, ct).ConfigureAwait(false);
            }
        }
        catch (OperationCanceledException) { }

        _target.Complete();

        _tcs.SetResult();
    }
}
