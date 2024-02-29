using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace TaskService.Entities;

public class Task
{
    [Key] [Required] public long Id { get; set; }

    [Required] public string Name { get; set; }

    [Required] public string Description { get; set; }

    [Required] public long IdProfession { get; set; }

    [Required] public long IdTeam { get; set; }

    [Required] public long IdCreator { get; set; }

    [Required]
    public long IdStatus { get; set; }
    
    [ForeignKey("IdStatus")]
    public Status Status { get; set; }
    
    [ForeignKey("IdCreator")]
    public User Creator { get; set; }
    
    [ForeignKey("IdTeam")]
    public Team Team { get; set; }
    
    [ForeignKey("IdProfession")]
    public Profession Profession { get; set; }
    
}