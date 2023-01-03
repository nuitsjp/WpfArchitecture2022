using UnitGenerator;

namespace AdventureWorks;

/// <summary>
/// Weight
/// </summary>
[UnitOf(typeof(decimal))]
public partial struct Gram
{
    public static Gram operator *(Gram gram, int quantity)
        => new(gram.value * quantity);
}