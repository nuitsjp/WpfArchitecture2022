namespace AdventureWorks.Production;

public class Product
{
    public Product(
        ProductId productId, 
        string name, 
        string productNumber, 
        string color, 
        Money standardPrice, 
        Money listPrice, 
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
    public Money StandardPrice { get; }
    public Money ListPrice { get; }
    public Gram Weight { get; }
    public ModifiedDateTime ModifiedDateTime { get; }
}