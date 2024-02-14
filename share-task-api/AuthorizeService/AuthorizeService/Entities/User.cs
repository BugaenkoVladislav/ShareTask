using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AuthorizeService.Entities;


[Index(nameof(Phone), IsUnique = true)]
[Index(nameof(IdLoginPassword), IsUnique = true)]
public class User
{
    [Key]
    [Required]
    public long Id { get; set; }
    
    [Required] 
    public string Name { get; set; }
    
    [Required] 
    public string Surname { get; set; }
    
    [Required]
    public long IdLoginPassword { get; set; }
    
    [Required]
    public string Phone { get; set; } 
    
    [Required]
    public long IdProfession { get; set; }
    
    
    
    [ForeignKey("IdLoginPassword")]
    public LoginPassword LoginPassword { get; set; }
    
    [ForeignKey("IdProfession")]
    public Profession Profession { get; set; }
}