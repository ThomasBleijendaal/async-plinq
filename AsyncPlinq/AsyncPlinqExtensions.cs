namespace AsyncPlinq;

public static partial class AsyncPlinqExtensions
{
    internal static Func<TInput, Task<TOutput>> MakeAsync<TInput, TOutput>(this Func<TInput, TOutput> method)
        => input => Task.FromResult(method.Invoke(input));

    internal static Func<TInput1, TInput2, TInput3, Task<TOutput>> MakeAsync<TInput1, TInput2, TInput3, TOutput>(this Func<TInput1, TInput2, TInput3, TOutput> method)
        => (input1, input2, input3) => Task.FromResult(method.Invoke(input1, input2, input3));
}
