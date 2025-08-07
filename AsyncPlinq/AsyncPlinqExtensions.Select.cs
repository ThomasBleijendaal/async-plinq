namespace AsyncPlinq;

public static partial class AsyncPlinqExtensions
{
    extension<TInput>(IEnumerable<TInput> source)
    {
        public IAsyncEnumerable<TResult> SelectAsync<TResult>(
            Func<TInput, Task<TResult>> selector,
            int? maxDegreeOfParallelism = null,
            CancellationToken token = default)
        {
            var transform = BlockBuilder.Create(selector, maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform, token);
        }

        public IAsyncEnumerable<TResult> SelectAsync<TResult>(
            Func<TInput, int, CancellationToken, Task<TResult>> selector,
            int? maxDegreeOfParallelism = null,
            CancellationToken token = default)
        {
            var transform = BlockBuilder.Create(selector, maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform, token);
        }
    }

    extension<TInput>(IAsyncEnumerable<TInput> source)
    {
        public IAsyncEnumerable<TResult> SelectAsync<TResult>(
            Func<TInput, TResult> selector,
            int? maxDegreeOfParallelism = null,
            CancellationToken token = default)
        {
            var transform = BlockBuilder.Create(selector.MakeAsync(), maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform, token);
        }

        public IAsyncEnumerable<TResult> SelectAsync<TResult>(
            Func<TInput, int, TResult> selector,
            int? maxDegreeOfParallelism = null,
            CancellationToken token = default)
        {
            var transform = BlockBuilder.Create(selector.MakeAsync(), maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform, token);
        }

        public IAsyncEnumerable<TResult> SelectAsync<TResult>(
            Func<TInput, Task<TResult>> selector,
            int? maxDegreeOfParallelism = null,
            CancellationToken token = default)
        {
            var transform = BlockBuilder.Create(selector, maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform, token);
        }

        public IAsyncEnumerable<TResult> SelectAsync<TResult>(
            Func<TInput, int, Task<TResult>> selector,
            int? maxDegreeOfParallelism = null,
            CancellationToken token = default)
        {
            var transform = BlockBuilder.Create(selector, maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform, token);
        }
    }
}
