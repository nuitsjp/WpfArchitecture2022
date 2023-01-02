using UnitGenerator;

namespace AdventureWorks;

/// <summary>
/// 
/// </summary>
[UnitOf(typeof(DateTime), UnitGenerateOptions.DapperTypeHandler)]
public partial struct ModifiedDateTime
{
    public static readonly ModifiedDateTime Unregistered = new(DateTime.MinValue);
}