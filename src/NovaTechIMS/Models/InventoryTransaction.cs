using System;
using NovaTechIMS.Models.Enums;

namespace NovaTechIMS.Models;

/// <summary>
/// Append-only stock movement record (DR-TXN).
/// No update/delete after creation (enforced in later data-access milestones).
/// </summary>
public class InventoryTransaction
{
    public int TransactionID { get; set; }
    public TransactionType TransactionType { get; set; }
    public int ProductID { get; set; }
    public int Quantity { get; set; }
    public DateTime TransactionDate { get; set; }
    public decimal? UnitPrice { get; set; }

    /// <summary>Actual supplier on Stock-In (may differ from Product default supplier).</summary>
    public int? SupplierID { get; set; }

    /// <summary>Optional customer on Stock-Out.</summary>
    public int? CustomerID { get; set; }

    public int? PreviousQuantity { get; set; }
    public int? NewQuantity { get; set; }
    public int? Difference { get; set; }
    public string? Reason { get; set; }

    public string? Notes { get; set; }
    public int UserID { get; set; }
    public DateTime CreatedDateTime { get; set; }
}
