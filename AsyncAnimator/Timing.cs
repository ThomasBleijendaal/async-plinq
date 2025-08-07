namespace AsyncAnimator;

internal record Timing(
    int Item,
    int Stage,
    bool Continues,
    TimeSpan Entry,
    TimeSpan Exit);
