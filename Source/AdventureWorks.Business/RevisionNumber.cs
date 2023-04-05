using UnitGenerator;

namespace AdventureWorks.Business;

/// <summary>
/// 
/// </summary>
[UnitOf(typeof(short))]
public partial struct RevisionNumber
{
    public static readonly RevisionNumber Unregistered = new(short.MinValue);
}
