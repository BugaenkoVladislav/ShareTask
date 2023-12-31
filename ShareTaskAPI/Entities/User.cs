﻿using System;
using System.Collections.Generic;

namespace ShareTaskAPI.Entities;

public partial class User
{
    public long IdUser { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = "1";

    public string Firstname { get; set; } = "1";

    public string Lastname { get; set; } = "1";

    public string Midname { get; set; } = "1";

    public bool IsAdmin { get; set; }

    public virtual ICollection<UserList> UsersLists { get; set; } = new List<UserList>();
    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
    public virtual ICollection<List> Lists { get; set; } = new List<List>();
}
