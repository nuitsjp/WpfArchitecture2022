namespace AdventureWorks.Business;

/// <summary>
/// Dollarの拡張メソッドクラス
/// </summary>
public static class DollarExtensions
{
    /// <summary>
    /// 合計を計算する。
    /// </summary>
    /// <param name="dollars"></param>
    /// <returns></returns>
    public static Dollar Sum(this IEnumerable<Dollar> dollars)
        => new(dollars.Sum(x => x.AsPrimitive()));

    /// <summary>
    /// 合計を計算する。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    public static Dollar Sum<T>(this IEnumerable<T> items, Func<T, Dollar> func)
    {
        return items.Select(func).Sum();
    }
}