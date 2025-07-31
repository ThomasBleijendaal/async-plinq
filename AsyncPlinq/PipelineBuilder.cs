namespace AsyncPlinq;

internal static class PipelineBuilder
{
    public static PipelineEnumerable<TOutput> CreateEnumerable<TInput, TOutput>(
        IEnumerable<TInput> source,
        Block<TInput, TOutput> block,
        CancellationToken token)
    {
        var input = new EnumerableSourceBlock<TInput>(source, token);
        input.LinkTo(block.Input, BlockOptions.DefaultOptions);

        return new PipelineEnumerable<TOutput>(block.Output, input);
    }

    public static PipelineEnumerable<TOutput> CreateEnumerable<TInput, TOutput>(
        IAsyncEnumerable<TInput> source,
        Block<TInput, TOutput> block,
        CancellationToken token)
    {
        if (source is PipelineEnumerable<TInput> pipeline)
        {
            pipeline.SourceBlock.LinkTo(block.Input, BlockOptions.DefaultOptions);
            return new PipelineEnumerable<TOutput>(block.Output, pipeline);
        }
        else
        {
            var input = new AsyncEnumerableSourceBlock<TInput>(source, token);
            input.LinkTo(block.Input, BlockOptions.DefaultOptions);

            return new PipelineEnumerable<TOutput>(block.Output, input);
        }
    }
}
