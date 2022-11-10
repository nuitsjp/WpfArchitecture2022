﻿using AdventureWorks.Production;

namespace AdventureWorks.Purchasing;

public class VendorProduct
{
    public VendorProduct(
        ProductId productId, 
        Days averageLeadTime, 
        Money standardPrice, 
        Money lastReceiptCost, 
        Date lastReceipt, 
        int minOrderQuantity, 
        int maxOrderQuantity, 
        int onOrderQuantity, 
        UnitMeasureCode unitMeasureCode, 
        ModifiedDateTime modifiedDateTime)
    {
        ProductId = productId;
        AverageLeadTime = averageLeadTime;
        StandardPrice = standardPrice;
        LastReceiptCost = lastReceiptCost;
        LastReceipt = lastReceipt;
        MinOrderQuantity = minOrderQuantity;
        MaxOrderQuantity = maxOrderQuantity;
        OnOrderQuantity = onOrderQuantity;
        UnitMeasureCode = unitMeasureCode;
        ModifiedDateTime = modifiedDateTime;
    }

    public ProductId ProductId { get; }
    public Days AverageLeadTime { get; }
    public Money StandardPrice { get; }
    public Money LastReceiptCost { get; }
    public Date LastReceipt { get; }
    public int MinOrderQuantity { get; }
    public int MaxOrderQuantity { get; }
    public int OnOrderQuantity { get; }
    public UnitMeasureCode UnitMeasureCode { get; }
    public ModifiedDateTime ModifiedDateTime { get; }
}