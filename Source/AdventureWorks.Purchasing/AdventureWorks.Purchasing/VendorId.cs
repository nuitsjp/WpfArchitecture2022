using UnitGenerator;

namespace AdventureWorks.Purchasing;

/// <summary>
/// ID of Vendor
/// </summary>
[UnitOf(typeof(int), UnitGenerateOptions.DapperTypeHandler)]
public partial struct VendorId
{
}