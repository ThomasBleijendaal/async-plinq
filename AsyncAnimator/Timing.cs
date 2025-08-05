namespace AsyncAnimator;

internal record Timing(
    int Item,
    int Stage,
    TimeSpan Entry,
    TimeSpan Exit);
