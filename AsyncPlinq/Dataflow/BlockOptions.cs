using System.Threading.Tasks.Dataflow;

namespace AsyncPlinq.Dataflow;

internal static class BlockOptions
{
    public static readonly DataflowLinkOptions DefaultOptions = new()
    {
        PropagateCompletion = true
    };

    public static readonly ExecutionDataflowBlockOptions MaxParallelOptions = new()
    {
        MaxDegreeOfParallelism = 32
    };
}
