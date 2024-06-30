﻿using System;
using System.Collections.Generic;

namespace Orders.API.Models;

public partial class OrderItem
{
    public int OrderId { get; set; }

    public int MealId { get; set; }

    public int Id { get; set; }

    public int Quantity { get; set; }

    public virtual Meal Meal { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
