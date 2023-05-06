namespace AdventureWorks.Business.Purchasing;

/// <summary>
/// 製品
/// </summary>
/// <param name="ProductId"></param>
/// <param name="Name"></param>
/// <param name="ProductNumber"></param>
/// <param name="Color"></param>
/// <param name="StandardPrice"></param>
/// <param name="ListPrice"></param>
/// <param name="Weight"></param>
/// <param name="ModifiedDateTime"></param>
public record Product(
        ProductId ProductId, 
        string Name, 
        string ProductNumber, 
        string Color, 
        Dollar StandardPrice, 
        Dollar ListPrice, 
        Gram Weight, 
        ModifiedDateTime ModifiedDateTime);