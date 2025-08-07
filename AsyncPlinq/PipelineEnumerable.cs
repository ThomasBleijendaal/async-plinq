using System.Threading.Tasks.Dataflow;

namespace AsyncPlinq;

internal class PipelineEnumerable<T> : IAsyncEnumerable<T>, IAsyncEnumerator<T>, IUpstreamBlock
{
    private T? _current;
    private readonly IUpstreamBlock? _upstreamBlock;

    private readonly Lock _completionLock = new();
    private bool _isCompleted;

    public PipelineEnumerable(
        ISourceBlock<T> sourceBlock,
        IUpstreamBlock? upstreamBlock)
    {
        SourceBlock = sourceBlock;
        _upstreamBlock = upstreamBlock;
    }

    public ISourceBlock<T> SourceBlock { get; }

    public void Complete()
    {
        lock (_completionLock)
        {
            if (_isCompleted)
            {
                return;
            }

            if (_upstreamBlock == null)
            {
                SourceBlock.Complete();
            }
            else
            {
                _upstreamBlock.Complete();
            }

            _isCompleted = true;
        }
    }

    public T Current
    {
        get => _current ?? throw new InvalidOperationException("No item");
        private set => _current = value;
    }

    public async ValueTask DisposeAsync()
    {
        _current = default;

        SourceBlock.Complete();

        if (_upstreamBlock != null)
        {
            await _upstreamBlock.DisposeAsync().ConfigureAwait(false);
        }
    }

    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        return this;
    }

    public async ValueTask<bool> MoveNextAsync()
    {
        if (!_isCompleted)
        {
            Complete();
        }

        if (!await SourceBlock.OutputAvailableAsync().ConfigureAwait(false))
        {
            return false;
        }

        Current = await SourceBlock.ReceiveAsync().ConfigureAwait(false);

        return true;
    }
}
