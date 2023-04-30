namespace AdventureWorks.Business.Purchasing;

public record Product(
        ProductId ProductId, 
        string Name, 
        string ProductNumber, 
        string Color, 
        Dollar StandardPrice, 
        Dollar ListPrice, 
        Gram Weight, 
        ModifiedDateTime ModifiedDateTime);