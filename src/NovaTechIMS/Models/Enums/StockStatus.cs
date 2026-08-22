namespace NovaTechIMS.Models.Enums;

/// <summary>
/// Derived stock level for a product (not stored in the database).
/// 0 on hand → OutOfStock; ≤ minimum → LowStock; otherwise InStock.
/// </summary>
public enum StockStatus
{
    OutOfStock = 0,
    LowStock = 1,
    InStock = 2
}
