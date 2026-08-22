namespace NovaTechIMS.Security;

/// <summary>
/// Permission keys used by AuthorizationService (ADR-005).
/// </summary>
public static class Permissions
{
    public const string ViewDashboard = "ViewDashboard";

    public const string ViewProducts = "ViewProducts";
    public const string ManageProducts = "ManageProducts";       // create/update (not delete)
    public const string DeleteProducts = "DeleteProducts";       // hard delete / deactivate

    public const string ViewCategories = "ViewCategories";
    public const string ManageCategories = "ManageCategories";
    public const string DeleteCategories = "DeleteCategories";

    public const string ViewSuppliers = "ViewSuppliers";
    public const string ManageSuppliers = "ManageSuppliers";
    public const string DeleteSuppliers = "DeleteSuppliers";

    public const string ViewCustomers = "ViewCustomers";
    public const string ManageCustomers = "ManageCustomers";
    public const string DeleteCustomers = "DeleteCustomers";

    public const string StockIn = "StockIn";
    public const string StockOut = "StockOut";
    public const string ViewInventoryHistory = "ViewInventoryHistory";
    public const string PerformAdjustment = "PerformAdjustment"; // non-delegatable

    public const string ViewReports = "ViewReports";

    public const string ManageUsers = "ManageUsers";             // non-delegatable
    public const string ManageDelegations = "ManageDelegations"; // non-delegatable
}
