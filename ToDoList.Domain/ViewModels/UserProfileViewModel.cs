using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Domain.ViewModels;

public class UserProfileViewModel
{
    [Display(Name = "Username")]
    [Required(ErrorMessage = "The username is required")]
    [StringLength(20)]
    [MinLength(3, ErrorMessage = "The min length of username is 3")]
    [Remote("CheckUserName", "Account", ErrorMessage = "This username already is use")]
    public string UserName { get; set; } = string.Empty;
    public UserPasswordViewModel? UserPasswordViewModel { get; set; }
}