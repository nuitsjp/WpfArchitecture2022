using UnitGenerator;

namespace AdventureWorks.Business;

/// <summary>
/// ���r�W�����i���o�[
/// </summary>
[UnitOf(typeof(short))]
public partial struct RevisionNumber
{
    /// <summary>
    /// ���o�^��\�����r�W�����i���o�[
    /// </summary>
    public static readonly RevisionNumber Unregistered = new(short.MinValue);
}
