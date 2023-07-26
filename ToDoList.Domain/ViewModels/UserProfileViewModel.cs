namespace ToDoList.Domain.ViewModels;

public class UserProfileViewModel
{
    public string UserName { get; set; } = string.Empty;
    public UserPasswordViewModel? UserPasswordViewModel { get; set; }
}