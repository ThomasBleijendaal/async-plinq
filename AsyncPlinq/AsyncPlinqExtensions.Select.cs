namespace AsyncPlinq;

public static partial class AsyncPlinqExtensions
{
    extension<TInput>(IEnumerable<TInput> source)
    {
        public IAsyncEnumerable<TResult> SelectAsync<TResult>(
            Func<TInput, Task<TResult>> selector,
            int? maxDegreeOfParallelism = null,
            CancellationToken? token = null)
            => source.SelectAsync(selector.WithIndex(), maxDegreeOfParallelism, token);

        public IAsyncEnumerable<TResult> SelectAsync<TResult>(
            Func<TInput, int, Task<TResult>> selector,
            int? maxDegreeOfParallelism = null,
            CancellationToken? token = null)
        {
            var transform = BlockBuilder.Create(selector.Wrap(), maxDegreeOfParallelism);

            var enumerable = PipelineBuilder.CreateEnumerable(source, transform, token ?? default);

            return enumerable;
        }
    }

    extension<TInput>(IAsyncEnumerable<TInput> source)
    {
        public IAsyncEnumerable<TResult> SelectAsync<TResult>(
            Func<TInput, TResult> selector,
            int? maxDegreeOfParallelism = null,
            CancellationToken? token = null)
        {
            var transform = BlockBuilder.Create(selector.MakeAsync(), maxDegreeOfParallelism);

            var enumerable = PipelineBuilder.CreateEnumerable(source, transform, token ?? default);

            return enumerable;
        }

        public IAsyncEnumerable<TResult> SelectAsync<TResult>(
            Func<TInput, Task<TResult>> selector,
            int? maxDegreeOfParallelism = null,
            CancellationToken? token = null)
        {
            var transform = BlockBuilder.Create(selector, maxDegreeOfParallelism);

            var enumerable = PipelineBuilder.CreateEnumerable(source, transform, token ?? default);

            return enumerable;
        }
    }
}
