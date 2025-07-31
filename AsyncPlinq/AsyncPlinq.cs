namespace AsyncPlinq;

public static class AsyncPlinq
{
    public static int DefaultMaxDegreeOfParallelism = 5;

    public static int CapacityMultiplier = 5;

    public static int BoundedCapacity(int? maxDegreeOfParallelism)
        => (maxDegreeOfParallelism ?? DefaultMaxDegreeOfParallelism) * CapacityMultiplier;
}
