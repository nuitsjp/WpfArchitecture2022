namespace AdventureWorks.Business.Purchasing;

/// <summary>
/// 支払い方法
/// </summary>
/// <param name="ShipMethodId"></param>
/// <param name="Name"></param>
/// <param name="ShipBase"></param>
/// <param name="ShipRate"></param>
/// <param name="ModifiedDateTime"></param>
public record ShipMethod(
        ShipMethodId ShipMethodId, 
        string Name, 
        Dollar ShipBase,
        DollarPerGram ShipRate, 
        ModifiedDateTime ModifiedDateTime);