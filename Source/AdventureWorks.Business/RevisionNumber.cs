using UnitGenerator;

namespace AdventureWorks.Business;

/// <summary>
/// リビジョンナンバー
/// </summary>
[UnitOf(typeof(short))]
public partial struct RevisionNumber
{
    /// <summary>
    /// 未登録を表すリビジョンナンバー
    /// </summary>
    public static readonly RevisionNumber Unregistered = new(short.MinValue);
}
