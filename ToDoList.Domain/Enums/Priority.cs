using System.ComponentModel.DataAnnotations;

namespace ToDoList.Domain.Enums;

public enum Priority
{
    [Display(Name = "Without priority")]
    WithoutPriority,
    Low,
    Medium,
    High
}