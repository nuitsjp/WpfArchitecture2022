namespace AdventureWorks;

public static class DollarExtensions
{
    public static Dollar Sum(this IEnumerable<Dollar> dollars)
        => new(dollars.Sum(x => x.AsPrimitive()));

    public static Dollar Sum<T>(this IEnumerable<T> items, Func<T, Dollar> func)
    {
        return items.Select(func).Sum();
    }
}