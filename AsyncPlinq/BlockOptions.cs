using System.Threading.Tasks.Dataflow;

namespace AsyncPlinq;

internal static class BlockOptions
{
    public static readonly DataflowLinkOptions DefaultOptions = new()
    {
        PropagateCompletion = true
    };
}
