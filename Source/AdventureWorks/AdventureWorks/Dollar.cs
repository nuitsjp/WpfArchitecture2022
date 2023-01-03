using UnitGenerator;

namespace AdventureWorks;

/// <summary>
/// 
/// </summary>
[UnitOf(typeof(decimal))]
public partial struct Dollar
{
    public static Dollar operator +(Dollar z, Dollar w)
    {
        return new Dollar(z.value + w.value);
    }

    public static Dollar operator -(Dollar z, Dollar w)
    {
        return new Dollar(z.value - w.value);
    }

    public static Dollar operator *(Dollar z, int quantity)
    {
        return new Dollar(z.value * quantity);
    }

    public static Dollar operator /(Dollar z, int quantity)
    {
        return new Dollar(z.value / quantity);
    }

    public static Dollar operator *(Dollar z, TaxRate taxRate)
    {
        return new Dollar(z.value * taxRate.AsPrimitive() / 100);
    }
}
