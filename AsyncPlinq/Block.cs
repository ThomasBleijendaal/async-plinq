using System.Threading.Tasks.Dataflow;

namespace AsyncPlinq;

internal record Block<TInput, TOutput>(ITargetBlock<TInput> Input, ISourceBlock<TOutput> Output);
