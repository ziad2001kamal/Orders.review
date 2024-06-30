using System;
using System.Collections.Generic;

namespace Orders.API.Models;

public partial class Order
{
    public int Id { get; set; }

    public string CustomerId { get; set; } = null!;

    public int ResturantId { get; set; }

    public int ResturentId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool IsDelete { get; set; }

    public virtual AspNetUser Customer { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Resturent Resturent { get; set; } = null!;
}
