using System;

namespace NovaTechIMS.Models;

/// <summary>Supplier master data (DR-SUP).</summary>
public class Supplier
{
    public int SupplierID { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public string? ContactPerson { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
}
