using UnitGenerator;

namespace AdventureWorks.Production;

/// <summary>
/// ID of ProductCategory
/// </summary>
[UnitOf(typeof(int), UnitGenerateOptions.DapperTypeHandler)]
public partial struct ProductCategoryId
{
}