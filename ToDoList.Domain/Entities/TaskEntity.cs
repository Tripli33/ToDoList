using System.ComponentModel.DataAnnotations;
using ToDoList.Domain.Enums;
using ToDoList.Domain.Validation;
namespace ToDoList.Domain.Entities;

public class TaskEntity
{
    public long Id { get; set; }
    [Required(ErrorMessage = "The Title is required")]
    [StringLength(30)]
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    [Required(ErrorMessage = "The Dead line is required")]
    [FutureDate(ErrorMessage = "The Dead line cannot be a past date")]
    public DateTime DeadLine { get; set; }
    public Priority Priority { get; set; }
    public Status Status { get; set; }
    public long UserId { get; set; }
    public User User { get; set; } = null!;
}