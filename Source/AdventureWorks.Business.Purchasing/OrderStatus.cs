// ReSharper disable UnusedMember.Global
namespace AdventureWorks.Business.Purchasing;

/// <summary>
/// Order current status.
/// </summary>
public enum OrderStatus
{
    Pending = 1,
    Approved = 2,
    Rejected = 3,
    Complete = 4
}