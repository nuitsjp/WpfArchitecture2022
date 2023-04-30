namespace AdventureWorks.Business.Purchasing;

public record ShipMethod(
        ShipMethodId ShipMethodId, 
        string Name, 
        Dollar ShipBase,
        DollarPerGram ShipRate, 
        ModifiedDateTime ModifiedDateTime);