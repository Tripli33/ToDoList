using Microsoft.AspNetCore.Http;
using ToDoList.Domain.Entities;
using ToDoList.Domain.ViewModels;

namespace ToDoList.Service.Interfaces;

public interface IAccountService 
{
    Task SignInWithDefaultPrincipalAsync(User user, string authenticationScheme);
    Task SignInWithDefaultPrincipalAsync(UserLoginViewModel userLoginViewModel, string authenticationScheme);
    Task SignOutAsync(string authenticationScheme);
    bool VerifyUserLoginViewModel(UserLoginViewModel userLoginViewModel);
}