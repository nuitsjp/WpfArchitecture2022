using UnitGenerator;

namespace AdventureWorks.Production;

/// <summary>
/// ID of ProductSubcategory
/// </summary>
[UnitOf(typeof(int), UnitGenerateOptions.DapperTypeHandler)]
public partial struct ProductSubcategoryId
{
}