using UnitGenerator;

namespace AdventureWorks.Business;

/// <summary>
/// Dollar
/// </summary>
[UnitOf(typeof(decimal))]
public partial struct Dollar
{
    /// <summary>
    /// ���Z
    /// </summary>
    /// <param name="z"></param>
    /// <param name="w"></param>
    /// <returns></returns>
    public static Dollar operator +(Dollar z, Dollar w)
    {
        return new Dollar(z.value + w.value);
    }

    /// <summary>
    /// ���Z
    /// </summary>
    /// <param name="z"></param>
    /// <param name="w"></param>
    /// <returns></returns>
    public static Dollar operator -(Dollar z, Dollar w)
    {
        return new Dollar(z.value - w.value);
    }

    /// <summary>
    /// ��Z
    /// </summary>
    /// <param name="z"></param>
    /// <param name="quantity"></param>
    /// <returns></returns>
    public static Dollar operator *(Dollar z, Quantity quantity)
    {
        return new Dollar(z.value * quantity.AsPrimitive());
    }

    /// <summary>
    /// ���Z
    /// </summary>
    /// <param name="z"></param>
    /// <param name="quantity"></param>
    /// <returns></returns>
    public static Dollar operator /(Dollar z, Quantity quantity)
    {
        return new Dollar(z.value / quantity.AsPrimitive());
    }

    /// <summary>
    /// �Ŋz�̌v�Z
    /// </summary>
    /// <param name="z"></param>
    /// <param name="taxRate"></param>
    /// <returns></returns>
    public static Dollar operator *(Dollar z, TaxRate taxRate)
    {
        return new Dollar(z.value * taxRate.AsPrimitive() / 100);
    }
}
