using System;
using System.Collections.Generic;

namespace Entities.DbEntities;

public partial class TableSeating
{
    public int TableSeatingId { get; set; }

    public int TableNo { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<VisitDetail> VisitDetails { get; set; } = new List<VisitDetail>();
}
