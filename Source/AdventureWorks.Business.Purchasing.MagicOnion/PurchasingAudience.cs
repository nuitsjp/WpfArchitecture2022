using AdventureWorks.Authentication.Jwt;

namespace AdventureWorks.Business.Purchasing.MagicOnion;

public static class PurchasingAudience
{
    public static readonly Audience Instance = new("AdventureWorks.Business.Purchasing");

}