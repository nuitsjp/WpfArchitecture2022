using UnitGenerator;

namespace AdventureWorks;

/// <summary>
/// 
/// </summary>
[UnitOf(typeof(decimal), UnitGenerateOptions.DapperTypeHandler)]
public partial struct DollarPerGram
{
    public static Dollar operator *(DollarPerGram rate, Gram gram)
    {
        return new Dollar(rate.value * gram.AsPrimitive());
    }
}
