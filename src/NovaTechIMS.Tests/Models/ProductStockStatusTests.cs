using NovaTechIMS.Data;
using Xunit;

namespace NovaTechIMS.Tests.Models;

/// <summary>FR-STK calculated status labels on list rows.</summary>
public class ProductStockStatusTests
{
    [Fact]
    public void OutOfStock_When_Qty_Zero()
    {
        var row = new ProductListRow { QuantityOnHand = 0, MinimumStockLevel = 5 };
        Assert.Equal("Out of Stock", row.StockStatusLabel);
    }

    [Fact]
    public void LowStock_When_Qty_Between_One_And_Min()
    {
        var row = new ProductListRow { QuantityOnHand = 3, MinimumStockLevel = 5 };
        Assert.Equal("Low Stock", row.StockStatusLabel);
    }

    [Fact]
    public void InStock_When_Qty_Above_Min()
    {
        var row = new ProductListRow { QuantityOnHand = 10, MinimumStockLevel = 5 };
        Assert.Equal("In Stock", row.StockStatusLabel);
    }

    [Fact]
    public void LowStock_When_Qty_Equals_Min()
    {
        var row = new ProductListRow { QuantityOnHand = 5, MinimumStockLevel = 5 };
        Assert.Equal("Low Stock", row.StockStatusLabel);
    }
}
