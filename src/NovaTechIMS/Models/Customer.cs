using System;

namespace NovaTechIMS.Models;

/// <summary>Customer master data (DR-CUS). Optional on Stock-Out.</summary>
public class Customer
{
    public int CustomerID { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
}
