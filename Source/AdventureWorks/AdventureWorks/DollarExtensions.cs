namespace AdventureWorks;

public static class DollarExtensions
{
    public static Dollar Sum(this IEnumerable<Dollar> dollars)
        => new(dollars.Sum(x => x.AsPrimitive()));
}