using System.Threading.Tasks.Dataflow;

namespace AsyncPlinq;

internal class AsyncEnumerableSourceBlock<T> : ISourceBlock<T>, IUpstreamBlock
{
    private readonly IAsyncEnumerable<T> _source;

    private readonly TaskCompletionSource _tcs;
    private readonly CancellationTokenSource _cts;

    private ITargetBlock<T>? _target;

    public AsyncEnumerableSourceBlock(
        IAsyncEnumerable<T> source,
        CancellationToken token)
    {
        _source = source;

        _tcs = new();
        Completion = _tcs.Task;

        _cts = CancellationTokenSource.CreateLinkedTokenSource(token);
    }

    public Task Completion { get; private set; }

    public void Complete()
    {
        lock (_tcs)
        {
            if (Completion == _tcs.Task)
            {
                Completion = ReadEnumerableAsync();
            }
            else
            {
                throw new InvalidOperationException("Enumeration already started");
            }
        }
    }

    public ValueTask DisposeAsync()
    {
        _cts.Cancel();

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

    private async Task ReadEnumerableAsync()
    {
        if (_target == null)
        {
            throw new InvalidOperationException("No target");
        }

        await foreach (var item in _source.WithCancellation(_cts.Token).ConfigureAwait(false))
        {
            await _target.SendAsync(item).ConfigureAwait(false);
        }

        _target.Complete();

        _tcs.SetResult();
    }
}
