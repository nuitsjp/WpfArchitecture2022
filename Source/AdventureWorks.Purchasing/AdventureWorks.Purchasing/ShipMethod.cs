namespace AdventureWorks.Purchasing;

public class ShipMethod
{
    public ShipMethod(
        ShipMethodId shipMethodId, 
        string name, 
        Dollar shipBase, 
        Dollar shipRate, 
        ModifiedDateTime modifiedDateTime)
    {
        ShipMethodId = shipMethodId;
        Name = name;
        ShipBase = shipBase;
        ShipRate = shipRate;
        ModifiedDateTime = modifiedDateTime;
    }

    public ShipMethodId ShipMethodId { get; }
    public string Name { get; }
    public Dollar ShipBase { get; }
    public Dollar ShipRate { get; }
    public ModifiedDateTime ModifiedDateTime { get; }
}