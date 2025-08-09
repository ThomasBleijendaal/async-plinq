using System.Threading.Tasks.Dataflow;

namespace AsyncPlinq.Dataflow;

internal record Block<TInput, TOutput>(
    ITargetBlock<TInput> Input,
    ISourceBlock<TOutput> Output,
    CancellationTokenSource? Cts = null);
