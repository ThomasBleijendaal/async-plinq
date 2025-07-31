namespace AsyncPlinq;

/*
 * TODO: cancellation tokens everywhere
 * TODO: find way to not collide with async linq implementations
 * TODO: more overloads link async linq (index etc)
 * TODO: find way to incorporate WhenAllAsync
 */


public static partial class AsyncPlinqExtensions
{
    internal static T1 Fst<T1, T2>((T1 t1, T2 t2) input) => input.t1;
    internal static T2 Snd<T1, T2>((T1 t1, T2 t2) input) => input.t2;

    internal static bool NotFst<T2>((bool t1, T2 t2) input) => !input.t1;

    internal static Func<TInput, Task<TOutput>> MakeAsync<TInput, TOutput>(this Func<TInput, TOutput> method)
        => input => Task.FromResult(method.Invoke(input));
}
