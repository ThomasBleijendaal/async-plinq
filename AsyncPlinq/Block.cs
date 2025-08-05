using System.Threading.Tasks.Dataflow;

namespace AsyncPlinq;

internal record struct BlockData<T>(T Data, int Index);

internal record Block<TInput, TOutput>(
    ITargetBlock<BlockData<TInput>> Input,
    ISourceBlock<BlockData<TOutput>> Output);
