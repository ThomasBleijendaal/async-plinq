using AsyncPlinq.Dataflow;

namespace AsyncPlinq;

public static partial class AsyncPlinqExtensions
{
    extension<TInput>(IEnumerable<TInput> source)
    {
        public IAsyncEnumerable<TResult> SelectManyAsync<TResult>(
            Func<TInput, Task<IEnumerable<TResult>>> selector,
            int? maxDegreeOfParallelism = null)
        {
            var transform = BlockBuilder.Create(selector, maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform);
        }

        public IAsyncEnumerable<TResult> SelectManyAsync<TResult>(
            Func<TInput, int, CancellationToken, Task<IEnumerable<TResult>>> selector,
            int? maxDegreeOfParallelism = null)
        {
            var transform = BlockBuilder.Create(selector, maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform);
        }

        public IAsyncEnumerable<TResult> SelectManyAsync<TResult>(
            Func<TInput, IAsyncEnumerable<TResult>> selector,
            int? maxDegreeOfParallelism = null)
        {
            var transform = BlockBuilder.Create(selector, maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform);
        }

        public IAsyncEnumerable<TResult> SelectManyAsync<TResult>(
            Func<TInput, int, CancellationToken, IAsyncEnumerable<TResult>> selector,
            int? maxDegreeOfParallelism = null)
        {
            var transform = BlockBuilder.Create(selector, maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform);
        }
    }

    extension<TInput>(IAsyncEnumerable<TInput> source)
    {
        public IAsyncEnumerable<TResult> SelectManyAsync<TResult>(
            Func<TInput, IEnumerable<TResult>> selector,
            int? maxDegreeOfParallelism = null)
        {
            var transform = BlockBuilder.Create(selector.MakeAsync(), maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform);
        }

        public IAsyncEnumerable<TResult> SelectManyAsync<TResult>(
            Func<TInput, int, CancellationToken, IEnumerable<TResult>> selector,
            int? maxDegreeOfParallelism = null)
        {
            var transform = BlockBuilder.Create(selector.MakeAsync(), maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform);
        }


        public IAsyncEnumerable<TResult> SelectManyAsync<TResult>(
            Func<TInput, Task<IEnumerable<TResult>>> selector,
            int? maxDegreeOfParallelism = null)
        {
            var transform = BlockBuilder.Create(selector, maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform);
        }


        public IAsyncEnumerable<TResult> SelectManyAsync<TResult>(
            Func<TInput, int, CancellationToken, Task<IEnumerable<TResult>>> selector,
            int? maxDegreeOfParallelism = null)
        {
            var transform = BlockBuilder.Create(selector, maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform);
        }
    }
}
