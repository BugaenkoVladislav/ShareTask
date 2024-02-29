using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskService.Entities;

public class TeamUser
{
    [Key]
    [Required]
    public long Id { get; set; }
    
    [Required]
    public long IdUser { get; set; }
    
    [Required]
    public long IdTeam { get; set; }

    [Required] 
    public bool IsAdmin { get; set; } = false;
    
    [ForeignKey("IdUser")]
    public User User { get; set; }
    
    [ForeignKey("IdTeam")]
    public Team Team { get; set; }
    
    
}