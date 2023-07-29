using System.Security.Claims;
using ToDoList.Domain.Entities;
using ToDoList.Domain.ViewModels;

namespace ToDoList.Service.Interfaces;

public interface IAccountService
{
    ClaimsPrincipal CreateClaimsPrincipal(List<Claim> claims, string authenticationType);
    Task<ClaimsPrincipal> CreateUserWithClaimsPrincipal(User user, List<Claim> claims, string authenticationType);
    Task<bool> VerifyUserViewModelAsync(UserViewModel userViewModel);
    Task<User> GetUserByUserNameAsync(string userName);
    Task<User> GetUserByEmailAsync(string email);
    Task<User> GetUserByEmailOrUserNameAsync(string emailOrUserName);
    Task UpdateUserNameAsync(User user, string userName);
    Task UpdateUserPasswordAsync(User user, string newPassword);
    Task UpdateUserNameByUserEmailAsync(string email, string userName);
    Task UpdateUserPasswordByUserEmailAsync(string email, string newPassword);
    Task DeleteUserByUserEmailAsync(string email);
}