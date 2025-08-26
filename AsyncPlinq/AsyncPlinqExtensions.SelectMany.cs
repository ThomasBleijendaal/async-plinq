using AsyncPlinq.Dataflow;

namespace AsyncPlinq;

public static partial class AsyncPlinqExtensions
{
    extension<TInput>(IEnumerable<TInput> source)
    {
        /// <summary>
        /// Projects each element of a sequence and flattens the resulting sequences into one sequence in parallel.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="maxDegreeOfParallelism"></param>
        /// <returns></returns>
        public IAsyncEnumerable<TResult> SelectManyAsync<TResult>(
            Func<TInput, Task<IEnumerable<TResult>>> selector,
            int? maxDegreeOfParallelism = null)
        {
            var transform = BlockBuilder.Create(selector, maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform);
        }

        /// <summary>
        /// Projects each element of a sequence and flattens the resulting sequences into one sequence in parallel.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="maxDegreeOfParallelism"></param>
        /// <returns></returns>
        public IAsyncEnumerable<TResult> SelectManyAsync<TResult>(
            Func<TInput, int, CancellationToken, Task<IEnumerable<TResult>>> selector,
            int? maxDegreeOfParallelism = null)
        {
            var transform = BlockBuilder.Create(selector, maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform);
        }

        /// <summary>
        /// Projects each element of a sequence and flattens the resulting sequences into one sequence in parallel.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="maxDegreeOfParallelism"></param>
        /// <returns></returns>
        public IAsyncEnumerable<TResult> SelectManyAsync<TResult>(
            Func<TInput, IAsyncEnumerable<TResult>> selector,
            int? maxDegreeOfParallelism = null)
        {
            var transform = BlockBuilder.Create(selector, maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform);
        }

        /// <summary>
        /// Projects each element of a sequence and flattens the resulting sequences into one sequence in parallel.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="maxDegreeOfParallelism"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Projects each element of a sequence and flattens the resulting sequences into one sequence in parallel.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="maxDegreeOfParallelism"></param>
        /// <returns></returns>
        public IAsyncEnumerable<TResult> SelectManyAsync<TResult>(
            Func<TInput, IEnumerable<TResult>> selector,
            int? maxDegreeOfParallelism = null)
        {
            var transform = BlockBuilder.Create(selector.MakeAsync(), maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform);
        }

        /// <summary>
        /// Projects each element of a sequence and flattens the resulting sequences into one sequence in parallel.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="maxDegreeOfParallelism"></param>
        /// <returns></returns>
        public IAsyncEnumerable<TResult> SelectManyAsync<TResult>(
            Func<TInput, int, CancellationToken, IEnumerable<TResult>> selector,
            int? maxDegreeOfParallelism = null)
        {
            var transform = BlockBuilder.Create(selector.MakeAsync(), maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform);
        }

        /// <summary>
        /// Projects each element of a sequence and flattens the resulting sequences into one sequence in parallel.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="maxDegreeOfParallelism"></param>
        /// <returns></returns>
        public IAsyncEnumerable<TResult> SelectManyAsync<TResult>(
            Func<TInput, Task<IEnumerable<TResult>>> selector,
            int? maxDegreeOfParallelism = null)
        {
            var transform = BlockBuilder.Create(selector, maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform);
        }

        /// <summary>
        /// Projects each element of a sequence and flattens the resulting sequences into one sequence in parallel.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="maxDegreeOfParallelism"></param>
        /// <returns></returns>
        public IAsyncEnumerable<TResult> SelectManyAsync<TResult>(
            Func<TInput, int, CancellationToken, Task<IEnumerable<TResult>>> selector,
            int? maxDegreeOfParallelism = null)
        {
            var transform = BlockBuilder.Create(selector, maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform);
        }

        /// <summary>
        /// Projects each element of a sequence and flattens the resulting sequences into one sequence in parallel.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="maxDegreeOfParallelism"></param>
        /// <returns></returns>
        public IAsyncEnumerable<TResult> SelectManyAsync<TResult>(
            Func<TInput, IAsyncEnumerable<TResult>> selector,
            int? maxDegreeOfParallelism = null)
        {
            var transform = BlockBuilder.Create(selector, maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform);
        }

        /// <summary>
        /// Projects each element of a sequence and flattens the resulting sequences into one sequence in parallel.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="maxDegreeOfParallelism"></param>
        /// <returns></returns>
        public IAsyncEnumerable<TResult> SelectManyAsync<TResult>(
            Func<TInput, int, CancellationToken, IAsyncEnumerable<TResult>> selector,
            int? maxDegreeOfParallelism = null)
        {
            var transform = BlockBuilder.Create(selector, maxDegreeOfParallelism);
            return PipelineBuilder.CreateEnumerable(source, transform);
        }
    }
}
