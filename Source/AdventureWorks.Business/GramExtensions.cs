namespace AdventureWorks.Business;

/// <summary>
/// Gram�Ɋւ���g�����\�b�h
/// </summary>
public static class GramExtensions
{
    /// <summary>
    /// ���v���v�Z����B
    /// </summary>
    /// <param name="grams"></param>
    /// <returns></returns>
    public static Gram Sum(this IEnumerable<Gram> grams)
        => new(grams.Sum(x => x.AsPrimitive()));

    /// <summary>
    /// ���v���v�Z����B
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    public static Gram Sum<T>(this IEnumerable<T> items, Func<T, Gram> func)
    {
        return items.Select(func).Sum();
    }

}