namespace NovaTechIMS.Models.Enums;

/// <summary>
/// Responsibilities an Administrator may temporarily grant to Inventory Staff.
/// Does not include Adjustment, User Management, or master-data delete/deactivate.
/// </summary>
public enum DelegatableResponsibility
{
    StockIn = 0,
    StockOut = 1,
    ReportAccess = 2
}
