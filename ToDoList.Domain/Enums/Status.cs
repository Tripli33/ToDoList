using System.ComponentModel.DataAnnotations;

namespace ToDoList.Domain.Enums;

public enum Status
{
    [Display(Name = "Not started")]
    NotStarted,
    [Display(Name = "In progress")]
    InProgress,
    Completed,
    Overdue
}