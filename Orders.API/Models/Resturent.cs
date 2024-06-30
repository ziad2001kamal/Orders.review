using System;
using System.Collections.Generic;

namespace Orders.API.Models;

public partial class Resturent
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string LogoUrl { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public decimal? Latitude { get; set; }

    public decimal? Longtude { get; set; }

    public string Adderss { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool IsDelete { get; set; }

    public virtual ICollection<Meal> Meals { get; set; } = new List<Meal>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
