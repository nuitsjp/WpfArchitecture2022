namespace AdventureWorks.Purchasing;

public class ShipMethod
{
    public ShipMethod(
        ShipMethodId shipMethodId, 
        string name, 
        Money shipBase, 
        Money shipRate, 
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
    public Money ShipBase { get; }
    public Money ShipRate { get; }
    public ModifiedDateTime ModifiedDateTime { get; }
}