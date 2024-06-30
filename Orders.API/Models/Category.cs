using System;
using System.Collections.Generic;

namespace Orders.API.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool IsDelete { get; set; }

    public virtual ICollection<Meal> Meals { get; set; } = new List<Meal>();
}
