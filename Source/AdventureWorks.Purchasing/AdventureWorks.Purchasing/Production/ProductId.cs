using UnitGenerator;

namespace AdventureWorks.Purchasing.Production;

/// <summary>
/// ID of Product
/// </summary>
[UnitOf(typeof(int), UnitGenerateOptions.DapperTypeHandler)]
public partial struct ProductId
{
}