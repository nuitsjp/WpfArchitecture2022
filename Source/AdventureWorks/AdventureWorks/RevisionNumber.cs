using UnitGenerator;

namespace AdventureWorks;

/// <summary>
/// 
/// </summary>
[UnitOf(typeof(short), UnitGenerateOptions.DapperTypeHandler)]
public partial struct RevisionNumber
{
    public static readonly RevisionNumber Unregistered = new(short.MinValue);
}
