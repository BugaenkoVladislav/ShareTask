using System;
using System.Collections.Generic;

namespace ShareTaskAPI.Entities;

public partial class User
{
    public long IdUser { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Midname { get; set; } = null!;

    public double Balance { get; set; }

    public bool IsAdmin { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual ICollection<UsersList> UsersLists { get; set; } = new List<UsersList>();
}
