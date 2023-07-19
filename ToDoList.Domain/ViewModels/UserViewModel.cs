using System.ComponentModel.DataAnnotations;

namespace ToDoList.Domain.ViewModels;

public class UserViewModel
{
    [Required(ErrorMessage = "The Email is required")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = String.Empty;
    [Required(ErrorMessage = "The Password is required")]
    [MinLength(4)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = String.Empty;
}