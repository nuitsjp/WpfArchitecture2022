using UnitGenerator;

namespace AdventureWorks.Business;

/// <summary>
/// 
/// </summary>
[UnitOf(typeof(DateTime))]
public partial struct ModifiedDateTime
{
    public static readonly ModifiedDateTime Unregistered = new(DateTime.MinValue);
}