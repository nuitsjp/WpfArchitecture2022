using UnitGenerator;

namespace AdventureWorks.Business;

/// <summary>
/// Dollar
/// </summary>
[UnitOf(typeof(decimal))]
public partial struct Dollar
{
    /// <summary>
    /// â¡éZ
    /// </summary>
    /// <param name="z"></param>
    /// <param name="w"></param>
    /// <returns></returns>
    public static Dollar operator +(Dollar z, Dollar w)
    {
        return new Dollar(z.value + w.value);
    }

    /// <summary>
    /// å∏éZ
    /// </summary>
    /// <param name="z"></param>
    /// <param name="w"></param>
    /// <returns></returns>
    public static Dollar operator -(Dollar z, Dollar w)
    {
        return new Dollar(z.value - w.value);
    }

    /// <summary>
    /// èÊéZ
    /// </summary>
    /// <param name="z"></param>
    /// <param name="quantity"></param>
    /// <returns></returns>
    public static Dollar operator *(Dollar z, Quantity quantity)
    {
        return new Dollar(z.value * quantity.AsPrimitive());
    }

    /// <summary>
    /// èúéZ
    /// </summary>
    /// <param name="z"></param>
    /// <param name="quantity"></param>
    /// <returns></returns>
    public static Dollar operator /(Dollar z, Quantity quantity)
    {
        return new Dollar(z.value / quantity.AsPrimitive());
    }

    /// <summary>
    /// ê≈äzÇÃåvéZ
    /// </summary>
    /// <param name="z"></param>
    /// <param name="taxRate"></param>
    /// <returns></returns>
    public static Dollar operator *(Dollar z, TaxRate taxRate)
    {
        return new Dollar(z.value * taxRate.AsPrimitive() / 100);
    }
}
