namespace AsyncPlinq;

internal static class PipelineBuilder
{
    public static PipelineEnumerable<TOutput> CreateEnumerable<TInput, TOutput>(
        IEnumerable<TInput> source,
        Block<TInput, TOutput> block)
    {
        var input = new EnumerableSourceBlock<TInput>(source);
        input.LinkTo(block.Input, BlockOptions.DefaultOptions);

        return new PipelineEnumerable<TOutput>(block.Output, input);
    }

    public static PipelineEnumerable<TOutput> CreateEnumerable<TInput, TOutput>(
        IAsyncEnumerable<TInput> source,
        Block<TInput, TOutput> block)
    {
        if (source is PipelineEnumerable<TInput> pipeline)
        {
            pipeline.SourceBlock.LinkTo(block.Input, BlockOptions.DefaultOptions);
            return new PipelineEnumerable<TOutput>(block.Output, pipeline);
        }
        else
        {
            var input = new AsyncEnumerableSourceBlock<TInput>(source);
            input.LinkTo(block.Input, BlockOptions.DefaultOptions);

            return new PipelineEnumerable<TOutput>(block.Output, input);
        }
    }
}
