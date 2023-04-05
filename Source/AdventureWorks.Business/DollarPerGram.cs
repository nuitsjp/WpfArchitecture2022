using UnitGenerator;

namespace AdventureWorks.Business;

/// <summary>
/// 
/// </summary>
[UnitOf(typeof(decimal))]
public partial struct DollarPerGram
{
    public static Dollar operator *(DollarPerGram rate, Gram gram)
    {
        return new Dollar(rate.value * gram.AsPrimitive());
    }
}
