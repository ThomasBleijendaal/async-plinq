namespace AsyncPlinq;

public static partial class AsyncPlinqExtensions
{
    extension<TInput>(IEnumerable<TInput> source)
    {
        public IAsyncEnumerable<TResult> SelectManyAsync<TResult>(
            Func<TInput, Task<IEnumerable<TResult>>> selector,
            int? maxDegreeOfParallelism = null,
            CancellationToken? token = null)
        {
            var transform = BlockBuilder.Create(selector, maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform, token ?? default);
        }

        public IAsyncEnumerable<TResult> SelectManyAsync<TResult>(
            Func<TInput, int, Task<IEnumerable<TResult>>> selector,
            int? maxDegreeOfParallelism = null,
            CancellationToken? token = null)
        {
            var transform = BlockBuilder.Create(selector, maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform, token ?? default);
        }
    }

    extension<TInput>(IAsyncEnumerable<TInput> source)
    {
        public IAsyncEnumerable<TResult> SelectManyAsync<TResult>(
            Func<TInput, IEnumerable<TResult>> selector,
            int? maxDegreeOfParallelism = null,
            CancellationToken? token = null)
        {
            var transform = BlockBuilder.Create(selector.MakeAsync(), maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform, token ?? default);
        }

        public IAsyncEnumerable<TResult> SelectManyAsync<TResult>(
            Func<TInput, int, IEnumerable<TResult>> selector,
            int? maxDegreeOfParallelism = null,
            CancellationToken? token = null)
        {
            var transform = BlockBuilder.Create(selector.MakeAsync(), maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform, token ?? default);
        }


        public IAsyncEnumerable<TResult> SelectManyAsync<TResult>(
            Func<TInput, Task<IEnumerable<TResult>>> selector,
            int? maxDegreeOfParallelism = null,
            CancellationToken? token = null)
        {
            var transform = BlockBuilder.Create(selector, maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform, token ?? default);
        }


        public IAsyncEnumerable<TResult> SelectManyAsync<TResult>(
            Func<TInput, int, Task<IEnumerable<TResult>>> selector,
            int? maxDegreeOfParallelism = null,
            CancellationToken? token = null)
        {
            var transform = BlockBuilder.Create(selector, maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform, token ?? default);
        }
    }
}
