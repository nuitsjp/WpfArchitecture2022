using UnitGenerator;

namespace AdventureWorks.Production;

/// <summary>
/// ID of Product
/// </summary>
[UnitOf(typeof(int), UnitGenerateOptions.DapperTypeHandler)]
public partial struct ProductId
{
}