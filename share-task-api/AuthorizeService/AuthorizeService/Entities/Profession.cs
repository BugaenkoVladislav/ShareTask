using System.ComponentModel.DataAnnotations;

namespace AuthorizeService.Entities;

public class Profession
{
    [Key]
    [Required]
    public long Id { get; set; }
    [Required] 
    public string Title { get; set; }
}