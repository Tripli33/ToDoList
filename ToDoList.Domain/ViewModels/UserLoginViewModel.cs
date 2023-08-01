using System.ComponentModel.DataAnnotations;

namespace ToDoList.Domain.ViewModels;

public class UserLoginViewModel
{
    [Required(ErrorMessage = "Email or username is required")]
    public string EmailOrUserName { get; set; } = String.Empty;
    [Required(ErrorMessage = "The password is required")]
    [MinLength(4)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = String.Empty;
}