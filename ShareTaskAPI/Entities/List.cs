namespace ShareTaskAPI.Entities;

public class List
{
    public long IdList { get; set; }
    public long IdCreator { get; set; }
    public string Name { get; set; } = null!;
    public bool IsPublic { get; set; }
    public string Linq { get; set; }= null!;
    public string Description { get; set; }= null!;
    
    public virtual ICollection<UserList> UsersLists { get; set; } = new List<UserList>();
    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
    public virtual User? IdCreatorNavigation { get; set; } = null!;
}