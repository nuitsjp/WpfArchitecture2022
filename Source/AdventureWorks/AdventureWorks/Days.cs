using UnitGenerator;

namespace AdventureWorks;

/// <summary>
/// Number of days
/// </summary>
[UnitOf(typeof(int), UnitGenerateOptions.DapperTypeHandler)]
public partial struct Days
{
}