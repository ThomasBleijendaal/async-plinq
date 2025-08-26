using AsyncPlinq.Dataflow;

namespace AsyncPlinq;

public static partial class AsyncPlinqExtensions
{
    extension<TInput>(IEnumerable<TInput> source)
    {
        /// <summary>
        /// Filters a sequence of values based on a predicate in parallel.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="maxDegreeOfParallelism"></param>
        /// <returns></returns>
        public IAsyncEnumerable<TInput> WhereAsync(
            Func<TInput, Task<bool>> predicate,
            int? maxDegreeOfParallelism = null)
        {
            var transform = BlockBuilder.Create(predicate, maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform);
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate in parallel.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="maxDegreeOfParallelism"></param>
        /// <returns></returns>
        public IAsyncEnumerable<TInput> WhereAsync(
            Func<TInput, int, CancellationToken, Task<bool>> predicate,
            int? maxDegreeOfParallelism = null)
        {
            var transform = BlockBuilder.Create(predicate, maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform);
        }
    }

    extension<TInput>(IAsyncEnumerable<TInput> source)
    {
        /// <summary>
        /// Filters a sequence of values based on a predicate in parallel.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="maxDegreeOfParallelism"></param>
        /// <returns></returns>
        public IAsyncEnumerable<TInput> WhereAsync(
            Func<TInput, bool> predicate,
            int? maxDegreeOfParallelism = null)
        {
            var transform = BlockBuilder.Create(predicate.MakeAsync(), maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform);
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate in parallel.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="maxDegreeOfParallelism"></param>
        /// <returns></returns>
        public IAsyncEnumerable<TInput> WhereAsync(
            Func<TInput, int, CancellationToken, bool> predicate,
            int? maxDegreeOfParallelism = null)
        {
            var transform = BlockBuilder.Create(predicate.MakeAsync(), maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform);
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate in parallel.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="maxDegreeOfParallelism"></param>
        /// <returns></returns>
        public IAsyncEnumerable<TInput> WhereAsync(
            Func<TInput, Task<bool>> predicate,
            int? maxDegreeOfParallelism = null)
        {
            var transform = BlockBuilder.Create(predicate, maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform);
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate in parallel.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="maxDegreeOfParallelism"></param>
        /// <returns></returns>
        public IAsyncEnumerable<TInput> WhereAsync(
            Func<TInput, int, CancellationToken, Task<bool>> predicate,
            int? maxDegreeOfParallelism = null)
        {
            var transform = BlockBuilder.Create(predicate, maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform);
        }
    }
}
