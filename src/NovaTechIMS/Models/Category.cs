using System;

namespace NovaTechIMS.Models;

/// <summary>Product category (DR-CAT).</summary>
public class Category
{
    public int CategoryID { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
}
