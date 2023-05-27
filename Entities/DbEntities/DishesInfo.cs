using System;
using System.Collections.Generic;

namespace Entities.DbEntities;

public partial class DishesInfo
{
    public int DishId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Price { get; set; }

    public string? ImageUrl { get; set; }

    public int? CookingTime { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
