using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskService.Entities;

public class TaskExecutor
{
    [Key]
    [Required]
    public long Id { get; set; }
    
    [Required]
    public long IdTask { get;set; }
    
    [Required]
    public long IdExecutor { get; set; }
    
    [Required]
    public string SubtaskDescription { get; set; }
    
    [Required]
    public long IdStatus { get; set; }
    
    [ForeignKey("IdStatus")]
    public Status Status { get; set; }
    
    [ForeignKey("IdExecutor")]
    public User Executor { get; set; }
    
    [ForeignKey("IdTask")]
    public Task Task { get; set; }
}