using System.Threading.Tasks.Dataflow;

namespace AsyncPlinq;

internal class EnumerableSourceBlock<T> : ISourceBlock<T>, IUpstreamBlock
{
    private readonly IEnumerable<T> _source;
    private readonly TaskCompletionSource _cts = new();
    private ITargetBlock<T>? _target;

    public EnumerableSourceBlock(
        IEnumerable<T> source)
    {
        _source = source;

        Completion = _cts.Task;
    }

    public Task Completion { get; private set; }

    public void Complete()
    {
        lock (_cts)
        {
            if (!Completion.IsCompleted)
            {
                ReadEnumerable();
            }
        }
    }

    public T? ConsumeMessage(DataflowMessageHeader messageHeader, ITargetBlock<T> target, out bool messageConsumed)
    {
        messageConsumed = false;
        return default;
    }

    public void Fault(Exception exception)
    {
        _cts.SetException(exception);
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

    private void ReadEnumerable()
    {
        if (_target == null)
        {
            throw new InvalidOperationException("No target");
        }

        foreach (var item in _source)
        {
            _target.Post(item);
        }

        _target.Complete();

        _cts.SetResult();
    }
}
