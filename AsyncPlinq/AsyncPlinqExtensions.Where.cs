namespace AsyncPlinq;

public static partial class AsyncPlinqExtensions
{
    extension<TInput>(IEnumerable<TInput> source)
    {
        public IAsyncEnumerable<TInput> WhereAsync(Func<TInput, Task<bool>> predicate, int? maxDegreeOfParallelism = null)
        {
            var transform = BlockBuilder.Create(predicate, maxDegreeOfParallelism);

            var enumerable = PipelineBuilder.CreateEnumerable(source, transform);

            return enumerable;
        }
    }

    extension<TInput>(IAsyncEnumerable<TInput> source)
    {
        public IAsyncEnumerable<TInput> WhereAsync(Func<TInput, bool> predicate, int? maxDegreeOfParallelism = null)
        {
            var transform = BlockBuilder.Create(predicate.MakeAsync(), maxDegreeOfParallelism);

            var enumerable = PipelineBuilder.CreateEnumerable(source, transform);

            return enumerable;
        }

        public IAsyncEnumerable<TInput> WhereAsync(Func<TInput, Task<bool>> predicate, int? maxDegreeOfParallelism = null)
        {
            var transform = BlockBuilder.Create(predicate, maxDegreeOfParallelism);

            var enumerable = PipelineBuilder.CreateEnumerable(source, transform);

            return enumerable;
        }
    }
}
