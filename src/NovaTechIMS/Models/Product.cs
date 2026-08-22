using System;
using NovaTechIMS.Models.Enums;

namespace NovaTechIMS.Models;

/// <summary>
/// Core inventory item (DR-PROD).
/// QuantityOnHand starts at 0 and is changed only by inventory operations (later milestones).
/// SupplierID is the default/preferred supplier (not necessarily the actual Stock-In supplier).
/// </summary>
public class Product
{
    public int ProductID { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int CategoryID { get; set; }
    public int SupplierID { get; set; }
    public string? Description { get; set; }
    public decimal PurchasePrice { get; set; }
    public decimal SellingPrice { get; set; }
    public int QuantityOnHand { get; set; }
    public int MinimumStockLevel { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public int? CreatedByUserID { get; set; }
    public int? ModifiedByUserID { get; set; }

    /// <summary>Derived status — not persisted.</summary>
    public StockStatus StockStatus
    {
        get
        {
            if (QuantityOnHand <= 0)
                return StockStatus.OutOfStock;
            if (QuantityOnHand <= MinimumStockLevel)
                return StockStatus.LowStock;
            return StockStatus.InStock;
        }
    }
}
