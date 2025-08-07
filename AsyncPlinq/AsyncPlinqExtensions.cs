namespace AsyncPlinq;

/*
 * TODO: cancellation tokens everywhere
 * TODO: find way to incorporate WhenAllAsync
 * TODO: improve disposing of where blocks - implement custom where block
 * TODO: parallelize tests
 * TODO: fix multiple enumeration exception not being thrown
 * TODO: implement source block for IEnumerable<Task<IEnumerable<T>>>
 */

public static partial class AsyncPlinqExtensions
{
    internal static T1 Fst<T1, T2>((T1 t1, T2 t2) input) => input.t1;
    internal static T2 Snd<T1, T2>((T1 t1, T2 t2) input) => input.t2;

    internal static bool NotFst<T2>((bool t1, T2 t2) input) => !input.t1;

    internal static Func<TInput, Task<TOutput>> MakeAsync<TInput, TOutput>(this Func<TInput, TOutput> method)
        => input => Task.FromResult(method.Invoke(input));

    internal static Func<TInput1, TInput2, Task<TOutput>> MakeAsync<TInput1, TInput2, TOutput>(this Func<TInput1, TInput2, TOutput> method)
        => (input1, input2) => Task.FromResult(method.Invoke(input1, input2));
}
