using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Domain.ViewModels;

public class UserProfileViewModel
{
    [Remote("CheckUserName", "Account", ErrorMessage = "This username already is use")]
    public string UserName { get; set; } = string.Empty;
    public UserPasswordViewModel? UserPasswordViewModel { get; set; }
}