using System;
using System.Collections.Generic;

namespace ShareTaskAPI.Entities;

public partial class List
{
    public long IdList { get; set; }

    public string Name { get; set; } = null!;

    public bool IsPublic { get; set; }

    public string? Link { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual ICollection<UsersList> UsersLists { get; set; } = new List<UsersList>();
}
