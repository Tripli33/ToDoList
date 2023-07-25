using Microsoft.VisualBasic.CompilerServices;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Domain.Entities;

public class User
{
    public long UserId { get; set; }
    [Display(Name = "Username")]
    [Required(ErrorMessage = "The Username is required")]
    [StringLength(20)]
    public string UserName { get; set; } = String.Empty;
    [Required(ErrorMessage = "The Email is required")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = String.Empty;
    [Required(ErrorMessage = "The Password is required")]
    [MinLength(4)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = String.Empty;
    [Display(Name = "Confirm password")]
    [Required(ErrorMessage = "The Confirm password is required")]
    [MinLength(4, ErrorMessage = "The min length of password is 4")]
    [DataType(DataType.Password)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; } = String.Empty;
    public ICollection<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
}