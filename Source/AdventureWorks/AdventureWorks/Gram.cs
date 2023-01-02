using UnitGenerator;

namespace AdventureWorks;

/// <summary>
/// Weight
/// </summary>
[UnitOf(typeof(decimal), UnitGenerateOptions.DapperTypeHandler)]
public partial struct Gram
{
    public static Gram operator *(Gram gram, int quantity)
        => new(gram.value * quantity);
}