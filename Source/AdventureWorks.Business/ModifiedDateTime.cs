using UnitGenerator;

namespace AdventureWorks.Business;

/// <summary>
/// 変更日時
/// </summary>
[UnitOf(typeof(DateTime))]
public partial struct ModifiedDateTime
{
    /// <summary>
    /// 未登録を表す変更日時
    /// </summary>
    public static readonly ModifiedDateTime Unregistered = new(DateTime.MinValue);
}