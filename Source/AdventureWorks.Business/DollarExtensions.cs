namespace AdventureWorks.Business;

/// <summary>
/// Dollar�̊g�����\�b�h�N���X
/// </summary>
public static class DollarExtensions
{
    /// <summary>
    /// ���v���v�Z����B
    /// </summary>
    /// <param name="dollars"></param>
    /// <returns></returns>
    public static Dollar Sum(this IEnumerable<Dollar> dollars)
        => new(dollars.Sum(x => x.AsPrimitive()));

    /// <summary>
    /// ���v���v�Z����B
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