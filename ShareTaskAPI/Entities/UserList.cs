using System.ComponentModel.DataAnnotations.Schema;

namespace ShareTaskAPI.Entities;

public class UserList
{
    public long IdUserList { get; set; }
    public long IdUser { get; set; }
    public long IdList { get; set; }
    
    public virtual User IdUserNavigation { get; set; } = null!;
    public virtual List IdListNavigation { get; set; } = null!;

}