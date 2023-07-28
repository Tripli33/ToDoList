using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Domain.Entities;
[Index(nameof(UserName), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]
public class User
{
    public long UserId { get; set; }
    [Display(Name = "Username")]
    [Required(ErrorMessage = "The Username is required")]
    [StringLength(20)]
    [MinLength(3, ErrorMessage = "The min length of username is 3")]
    [Remote("CheckUserName", "Account", ErrorMessage = "This username already is use")]
    public string UserName { get; set; } = String.Empty;
    [Required(ErrorMessage = "The Email is required")]
    [DataType(DataType.EmailAddress)]
    [Remote("CheckEmail", "Account", ErrorMessage = "This email already is use")]
    public string Email { get; set; } = String.Empty;
    [Required(ErrorMessage = "The Password is required")]
    [MinLength(4, ErrorMessage = "The min length of password is 4")]
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