namespace AsyncPlinq;

internal class DummyDisposable : IDisposable
{
    public static DummyDisposable Dummy = new();

    public void Dispose() { }
}
