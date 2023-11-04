using System;
using System.Collections.Generic;

namespace ShareTaskAPI.Entities;

public partial class Role
{
    public long IdRole { get; set; }

    public string Role1 { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
