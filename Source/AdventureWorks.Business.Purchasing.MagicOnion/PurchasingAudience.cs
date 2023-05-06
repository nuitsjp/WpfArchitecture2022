using AdventureWorks.Authentication.Jwt;

namespace AdventureWorks.Business.Purchasing.MagicOnion;

/// <summary>
/// 発注オーディエンス
/// </summary>
public static class PurchasingAudience
{
    /// <summary>
    /// シングルトンインスタンス
    /// </summary>
    public static readonly Audience Instance = new("AdventureWorks.Business.Purchasing");

}