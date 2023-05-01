using UnitGenerator;

namespace AdventureWorks.Business;

/// <summary>
/// 重量単価
/// </summary>
[UnitOf(typeof(decimal))]
public partial struct DollarPerGram
{
    /// <summary>
    /// 指定された重量の価格を計算する。
    /// </summary>
    /// <param name="rate"></param>
    /// <param name="gram"></param>
    /// <returns></returns>
    public static Dollar operator *(DollarPerGram rate, Gram gram)
    {
        return new Dollar(rate.value * gram.AsPrimitive());
    }
}
