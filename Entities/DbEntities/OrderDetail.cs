using System;
using System.Collections.Generic;

namespace Entities.DbEntities;

public partial class OrderDetail
{
    public int OrderId { get; set; }

    public int VisitId { get; set; }

    public int DishId { get; set; }

    public virtual DishesInfo Dish { get; set; } = null!;

    public virtual VisitDetail Visit { get; set; } = null!;
}
