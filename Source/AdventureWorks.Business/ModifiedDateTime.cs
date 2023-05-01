using UnitGenerator;

namespace AdventureWorks.Business;

/// <summary>
/// •ÏX“ú
/// </summary>
[UnitOf(typeof(DateTime))]
public partial struct ModifiedDateTime
{
    /// <summary>
    /// –¢“o˜^‚ğ•\‚·•ÏX“ú
    /// </summary>
    public static readonly ModifiedDateTime Unregistered = new(DateTime.MinValue);
}