using System.Security.Claims;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Enums;
using ToDoList.Domain.ViewModels;

namespace ToDoList.Service.Interfaces;

public interface IAccountService
{
    ClaimsPrincipal CreateClaimsPrincipal(List<Claim> claims, string authenticationType);
    Task<ClaimsPrincipal> CreateUserWithClaimsPrincipalAsync(User user, List<Claim> claims, string authenticationType);
    Task<bool> VerifyUserLoginViewModelAsync(UserLoginViewModel userLoginViewModel);
    Task UpdateUserRoleAsync(long userId, Role role);
    Task UpdateUserNameAsync(User user, string userName);
    Task UpdateUserPasswordAsync(User user, string newPassword);
    Task UpdateUserNameByEmailAsync(string email, string userName);
    Task UpdateUserPasswordByEmailAsync(string email, string newPassword);
    Task DeleteUserByEmailAsync(string email);
}