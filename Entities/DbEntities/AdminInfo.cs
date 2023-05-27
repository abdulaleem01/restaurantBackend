using System;
using System.Collections.Generic;

namespace Entities.DbEntities;

public partial class AdminInfo
{
    public int AdminId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}
