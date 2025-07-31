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

            var enumerable = PipelineBuilder.CreateEnumerable(source, transform, token ?? default);

            return enumerable;
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

            var enumerable = PipelineBuilder.CreateEnumerable(source, transform, token ?? default);

            return enumerable;
        }

        public IAsyncEnumerable<TInput> WhereAsync(
            Func<TInput, Task<bool>> predicate,
            int? maxDegreeOfParallelism = null,
            CancellationToken? token = null)
        {
            var transform = BlockBuilder.Create(predicate, maxDegreeOfParallelism);

            var enumerable = PipelineBuilder.CreateEnumerable(source, transform, token ?? default);

            return enumerable;
        }
    }
}
