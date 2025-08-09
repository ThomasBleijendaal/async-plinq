using System.Threading.Tasks.Dataflow;
using AsyncPlinq;

namespace AsyncPlinq.Dataflow;

internal class PipelineEnumerable<T> : IAsyncEnumerable<T>, IAsyncEnumerator<T>, IUpstreamBlock
{
    private T? _current;
    private readonly IUpstreamBlock? _upstreamBlock;

    private readonly CancellationTokenSource? _cts;

    private readonly Lock _completionLock = new();
    private bool _isCompleted;

    private bool _enumerationStarted = false;

    public PipelineEnumerable(
        ISourceBlock<T> sourceBlock,
        IUpstreamBlock? upstreamBlock,
        CancellationTokenSource? cts)
    {
        SourceBlock = sourceBlock;
        _upstreamBlock = upstreamBlock;
        _cts = cts;
    }

    public ISourceBlock<T> SourceBlock { get; }

    public void Complete() => Complete(default);

    public void Complete(CancellationToken token)
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
                _upstreamBlock.Complete(token);
            }

            _isCompleted = true;
        }
    }

    public T Current
    {
        get => _current ?? throw new InvalidOperationException("Enumeration not started yet");
        private set => _current = value;
    }

    public async ValueTask DisposeAsync()
    {
        _cts?.Cancel();

        _current = default;

        SourceBlock.Complete();

        if (_upstreamBlock != null)
        {
            await _upstreamBlock.DisposeAsync().ConfigureAwait(false);
        }
    }

    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        EnsureEnumerationNotStarted();

        lock (_completionLock)
        {
            EnsureEnumerationNotStarted();

            if (!_enumerationStarted)
            {
                _enumerationStarted = true;
            }
        }

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

    private void EnsureEnumerationNotStarted()
    {
        if (_enumerationStarted)
        {
            throw new InvalidOperationException("Enumeration already started");
        }
    }
}
