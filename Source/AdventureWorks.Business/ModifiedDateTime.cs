using UnitGenerator;

namespace AdventureWorks.Business;

/// <summary>
/// �ύX����
/// </summary>
[UnitOf(typeof(DateTime))]
public partial struct ModifiedDateTime
{
    /// <summary>
    /// ���o�^��\���ύX����
    /// </summary>
    public static readonly ModifiedDateTime Unregistered = new(DateTime.MinValue);
}