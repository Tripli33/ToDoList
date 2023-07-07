using System.ComponentModel.DataAnnotations;
using ToDoList.Domain.Enums;

namespace ToDoList.Domain.Entities;

public class Task
{
    public long Id { get; set; }
    [Required]
    [StringLength(30)]
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    // TODO: Date-time validation
    public DateTime DeadLine { get; set; } = DateTime.Today;
    public Priority Priority { get; set; }
    public Status Status { get; set; }
}