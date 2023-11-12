namespace ShareTaskAPI.Entities;

public class Task
{
    public long IdTask { get; set; }
    public long IdList { get; set; }
    public long IdCreator { get; set; }
    public long IdRole { get; set; }
    public string  NameTask { get; set; }= null!;
    public string Description { get; set; } = null!;

    public virtual List IdListNavigaton { get; set; } = null!;
    public virtual User IdCreatorNavigaton { get; set; } = null!;
    public virtual Role IdRoleNavigation { get; set; } = null!;
}