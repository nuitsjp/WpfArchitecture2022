using UnitGenerator;

namespace AdventureWorks.Production
{

    /// <summary>
    /// ID of Product
    /// </summary>
    [UnitOf(typeof(int), UnitGenerateOptions.DapperTypeHandler)]
    public partial struct ProductId
    {
    }

    /// <summary>
    /// ID of ProductCategory
    /// </summary>
    [UnitOf(typeof(int), UnitGenerateOptions.DapperTypeHandler)]
    public partial struct ProductCategoryId
    {
    }

    /// <summary>
    /// ID of ProductSubcategory
    /// </summary>
    [UnitOf(typeof(int), UnitGenerateOptions.DapperTypeHandler)]
    public partial struct ProductSubcategoryId
    {
    }

    /// <summary>
    /// Code of UnitMeasure
    /// </summary>
    [UnitOf(typeof(int), UnitGenerateOptions.DapperTypeHandler)]
    public partial struct UnitMeasureCode
    {
    }

}
