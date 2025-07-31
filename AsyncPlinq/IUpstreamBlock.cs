namespace AsyncPlinq;

internal interface IUpstreamBlock : IAsyncDisposable
{
    void Complete();
}
