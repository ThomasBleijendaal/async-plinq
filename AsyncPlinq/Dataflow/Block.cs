using System.Threading.Tasks.Dataflow;

namespace AsyncPlinq.Dataflow;

internal record Block<TInput, TOutput>(
    ITargetBlock<TInput> Input,
    IReceivableSourceBlock<TOutput> Output,
    CancellationTokenSource? Cts = null);
