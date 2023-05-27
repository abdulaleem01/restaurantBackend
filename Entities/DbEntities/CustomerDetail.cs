using System;
using System.Collections.Generic;

namespace Entities.DbEntities;

public partial class CustomerDetail
{
    public int CustomerId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<VisitDetail> VisitDetails { get; set; } = new List<VisitDetail>();
}
