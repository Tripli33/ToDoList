using Microsoft.VisualBasic.CompilerServices;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Domain.Entities;

public class User
{
    public long Id { get; set; }
    [Required]
    [StringLength(20)]
    public string UserName { get; set; } = String.Empty;
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = String.Empty;
    [Required]
    [MinLength(4)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = String.Empty;
    [Required]
    [MinLength(4)]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "")]
    public string ConfirmPassword { get; set; } = String.Empty;
    public ICollection<Task>? Tasks { get; set; }
}