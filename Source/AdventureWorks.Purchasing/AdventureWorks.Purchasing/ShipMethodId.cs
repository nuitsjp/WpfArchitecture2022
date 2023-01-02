using UnitGenerator;

namespace AdventureWorks.Purchasing;

/// <summary>
/// ID of ShipMethod
/// </summary>
[UnitOf(typeof(int), UnitGenerateOptions.DapperTypeHandler)]
public partial struct ShipMethodId
{
}