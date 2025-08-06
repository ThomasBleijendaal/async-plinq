namespace AsyncPlinq;

public static partial class AsyncPlinqExtensions
{
    extension<TInput>(IEnumerable<TInput> source)
    {
        public IAsyncEnumerable<TInput> WhereAsync(
            Func<TInput, Task<bool>> predicate,
            int? maxDegreeOfParallelism = null,
            CancellationToken? token = null)
        {
            var transform = BlockBuilder.Create(predicate, maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform, token ?? default);
        }

        public IAsyncEnumerable<TInput> WhereAsync(
            Func<TInput, int, Task<bool>> predicate,
            int? maxDegreeOfParallelism = null,
            CancellationToken? token = null)
        {
            var transform = BlockBuilder.Create(predicate, maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform, token ?? default);
        }
    }

    extension<TInput>(IAsyncEnumerable<TInput> source)
    {
        public IAsyncEnumerable<TInput> WhereAsync(
            Func<TInput, bool> predicate,
            int? maxDegreeOfParallelism = null,
            CancellationToken? token = null)
        {
            var transform = BlockBuilder.Create(predicate.MakeAsync(), maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform, token ?? default);
        }

        public IAsyncEnumerable<TInput> WhereAsync(
            Func<TInput, int, bool> predicate,
            int? maxDegreeOfParallelism = null,
            CancellationToken? token = null)
        {
            var transform = BlockBuilder.Create(predicate.MakeAsync(), maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform, token ?? default);
        }

        public IAsyncEnumerable<TInput> WhereAsync(
            Func<TInput, Task<bool>> predicate,
            int? maxDegreeOfParallelism = null,
            CancellationToken? token = null)
        {
            var transform = BlockBuilder.Create(predicate, maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform, token ?? default);
        }

        public IAsyncEnumerable<TInput> WhereAsync(
            Func<TInput, int, Task<bool>> predicate,
            int? maxDegreeOfParallelism = null,
            CancellationToken? token = null)
        {
            var transform = BlockBuilder.Create(predicate, maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform, token ?? default);
        }
    }
}
