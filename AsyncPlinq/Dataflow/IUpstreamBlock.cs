namespace AsyncPlinq.Dataflow;

internal interface IUpstreamBlock : IAsyncDisposable
{
    void Complete(CancellationToken token);
}
