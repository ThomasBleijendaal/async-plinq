using AsyncPlinq.Dataflow;

namespace AsyncPlinq;

public static partial class AsyncPlinqExtensions
{
    extension<TInput>(IEnumerable<TInput> source)
    {
        public IAsyncEnumerable<TResult> SelectAsync<TResult>(
            Func<TInput, Task<TResult>> selector,
            int? maxDegreeOfParallelism = null)
        {
            var transform = BlockBuilder.Create(selector, maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform);
        }

        public IAsyncEnumerable<TResult> SelectAsync<TResult>(
            Func<TInput, int, CancellationToken, Task<TResult>> selector,
            int? maxDegreeOfParallelism = null)
        {
            var transform = BlockBuilder.Create(selector, maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform);
        }
    }

    extension<TInput>(IAsyncEnumerable<TInput> source)
    {
        public IAsyncEnumerable<TResult> SelectAsync<TResult>(
            Func<TInput, TResult> selector,
            int? maxDegreeOfParallelism = null)
        {
            var transform = BlockBuilder.Create(selector.MakeAsync(), maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform);
        }

        public IAsyncEnumerable<TResult> SelectAsync<TResult>(
            Func<TInput, int, CancellationToken, TResult> selector,
            int? maxDegreeOfParallelism = null)
        {
            var transform = BlockBuilder.Create(selector.MakeAsync(), maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform);
        }

        public IAsyncEnumerable<TResult> SelectAsync<TResult>(
            Func<TInput, Task<TResult>> selector,
            int? maxDegreeOfParallelism = null)
        {
            var transform = BlockBuilder.Create(selector, maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform);
        }

        public IAsyncEnumerable<TResult> SelectAsync<TResult>(
            Func<TInput, int, CancellationToken, Task<TResult>> selector,
            int? maxDegreeOfParallelism = null)
        {
            var transform = BlockBuilder.Create(selector, maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform);
        }
    }
}
