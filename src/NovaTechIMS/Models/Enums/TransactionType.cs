namespace NovaTechIMS.Models.Enums;

/// <summary>Inventory movement kind (append-only history).</summary>
public enum TransactionType
{
    StockIn = 0,
    StockOut = 1,
    Adjustment = 2
}
