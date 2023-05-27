using System;
using System.Collections.Generic;

namespace Entities.DbEntities;

public partial class VisitDetail
{
    public int VisitId { get; set; }

    public int CustomerId { get; set; }

    public int TableId { get; set; }

    public string Date { get; set; } = null!;

    public string Time { get; set; } = null!;

    public int DeliveryStatus { get; set; }

    public int PaymentStatus { get; set; }

    public virtual CustomerDetail Customer { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual TableSeating Table { get; set; } = null!;
}
