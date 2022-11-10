namespace AdventureWorks;

public static class GramExtensions
{
    public static Gram Sum(this IEnumerable<Gram> grams)
        => new(grams.Sum(x => x.AsPrimitive()));
}