namespace AdventureWorks.Purchasing;

public class Product
{
    public Product(
        ProductId productId, 
        string name, 
        string productNumber, 
        string color, 
        Dollar standardPrice, 
        Dollar listPrice, 
        Gram weight, 
        ModifiedDateTime modifiedDateTime)
    {
        ProductId = productId;
        Name = name;
        ProductNumber = productNumber;
        Color = color;
        StandardPrice = standardPrice;
        ListPrice = listPrice;
        Weight = weight;
        ModifiedDateTime = modifiedDateTime;
    }

    public ProductId ProductId { get; }
    public string Name { get; }
    public string ProductNumber { get; }
    public string Color { get; }
    public Dollar StandardPrice { get; }
    public Dollar ListPrice { get; }
    public Gram Weight { get; }
    public ModifiedDateTime ModifiedDateTime { get; }
}