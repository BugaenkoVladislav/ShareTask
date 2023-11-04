using System;
using System.Collections.Generic;

namespace ShareTaskAPI.Entities;

public partial class Task
{
    public long IdTask { get; set; }

    public long IdList { get; set; }

    public string Task1 { get; set; } = null!;

    public long IdRole { get; set; }

    public string? Description { get; set; }

    public long IdUser { get; set; }

    public virtual List IdListNavigation { get; set; } = null!;

    public virtual Role IdRoleNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
