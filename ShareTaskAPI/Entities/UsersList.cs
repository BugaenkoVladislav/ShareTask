using System;
using System.Collections.Generic;

namespace ShareTaskAPI.Entities;

public partial class UsersList
{
    public long IdUsersLists { get; set; }

    public long IdUser { get; set; }

    public long IdList { get; set; }

    public virtual List IdListNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
