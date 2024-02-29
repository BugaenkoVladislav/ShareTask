using System.ComponentModel.DataAnnotations;

namespace TaskService.Entities;

public class Status
{
    [Key]
    [Required]
    public long Id { get; set; }
    
    [Required]
    public string Name { get; set; }
}