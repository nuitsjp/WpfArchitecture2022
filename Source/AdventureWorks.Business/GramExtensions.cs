namespace AdventureWorks.Business;

/// <summary>
/// Gramに関する拡張メソッド
/// </summary>
public static class GramExtensions
{
    /// <summary>
    /// 合計を計算する。
    /// </summary>
    /// <param name="grams"></param>
    /// <returns></returns>
    public static Gram Sum(this IEnumerable<Gram> grams)
        => new(grams.Sum(x => x.AsPrimitive()));

    /// <summary>
    /// 合計を計算する。
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