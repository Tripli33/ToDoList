using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Enums;
using ToDoList.Domain.ViewModels;
using ToDoList.Infrastructure;
using ToDoList.Infrastructure.Interfaces;
using ToDoList.Service.Interfaces;

namespace ToDoList.Service.Implementations;

public class AccountService : IAccountService
{
    private readonly IUserRepository _userRepository;

    public AccountService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ClaimsPrincipal> CreateUserWithClaimsPrincipalAsync(User user, List<Claim> claims, string authenticationType)
    {
        await _userRepository.CreateAsync(user);
        var claimsIdentity = new ClaimsIdentity(claims, authenticationType);
        return new ClaimsPrincipal(claimsIdentity);
    }
    public ClaimsPrincipal CreateClaimsPrincipal(List<Claim> claims, string authenticationType)
    {
        var claimsIdentity = new ClaimsIdentity(claims, authenticationType);
        return new ClaimsPrincipal(claimsIdentity);
    }

    public async Task<bool> VerifyUserLoginViewModelAsync(UserLoginViewModel userLoginViewModel)
    {
        var user = await _userRepository.GetUserByEmailOrUserNameAsync(userLoginViewModel.EmailOrUserName);
        return user is null || user.Password != userLoginViewModel.Password;
    }

    public async Task UpdateUserPasswordAsync(User user, string newPassword)
    {
        user.Password = newPassword;
        user.ConfirmPassword = newPassword;
        await _userRepository.UpdateAsync(user);
    }

    public async Task UpdateUserPasswordByEmailAsync(string email, string newPassword)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        await UpdateUserPasswordAsync(user, newPassword);
    }

    public async Task DeleteUserByEmailAsync(string email)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        await _userRepository.DeleteAsync(user);
    }

    public async Task UpdateUserNameAsync(User user, string userName)
    {
        user.UserName = userName;
        await _userRepository.UpdateAsync(user);
    }

    public async Task UpdateUserNameByEmailAsync(string email, string userName)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        await UpdateUserNameAsync(user, userName);
    }

    public async Task UpdateUserRoleAsync(long userId, Role role)
    {
        var user = await _userRepository.SelectAsync(userId);
        user.Role = role;
        _userRepository.UpdateAsync(user);
    }
}