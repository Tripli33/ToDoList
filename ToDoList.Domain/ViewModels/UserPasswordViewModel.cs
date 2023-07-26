using System.ComponentModel.DataAnnotations;

namespace ToDoList.Domain.ViewModels;

public class UserPasswordViewModel
{
    [Display(Name = "Old password")]
    [MinLength(4, ErrorMessage = "The min length of password is 4")]
    [DataType(DataType.Password)]
    public string OldPassword { get; set; } = string.Empty;
    [Display(Name = "New password")]
    [MinLength(4, ErrorMessage = "The min length of password is 4")]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; } = string.Empty;
    [Display(Name = "Confirm new password")]
    [Required(ErrorMessage = "The confirm new password is required")]
    [MinLength(4, ErrorMessage = "The min length of password is 4")]
    [DataType(DataType.Password)]
    [Compare("NewPassword")]
    public string ConfirmNewPassword { get; set; } = string.Empty;
}