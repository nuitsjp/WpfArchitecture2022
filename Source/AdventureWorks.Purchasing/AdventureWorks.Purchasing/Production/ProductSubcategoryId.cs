using UnitGenerator;

namespace AdventureWorks.Purchasing.Production;

/// <summary>
/// ID of ProductSubcategory
/// </summary>
[UnitOf(typeof(int), UnitGenerateOptions.DapperTypeHandler)]
public partial struct ProductSubcategoryId
{
}