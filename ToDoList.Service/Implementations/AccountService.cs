using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entities;
using ToDoList.Domain.ViewModels;
using ToDoList.Infrastructure;
using ToDoList.Infrastructure.Interfaces;
using ToDoList.Service.Interfaces;

namespace ToDoList.Service.Implementations;

public class AccountService : IAccountService
{
    private readonly IUserRepository _userRepository;
    private readonly ApplicationContext _applicationContext;

    public AccountService(ApplicationContext applicationContext, IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _applicationContext = applicationContext;
    }

    public async Task<ClaimsPrincipal> CreateUserWithClaimsPrincipal(User user, List<Claim> claims, string authenticationType)
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

    public async Task<bool> VerifyUserViewModelAsync(UserViewModel userViewModel)
    {
        var user = await GetUserByEmailAsync(userViewModel.Email);
        return user is null || user.Password != userViewModel.Password;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        var user = await _applicationContext.Users.FirstOrDefaultAsync(user => user.Email == email);
        return user;
    }

    public async Task UpdateUserPasswordAsync(User user, string newPassword)
    {
        user.Password = newPassword;
        user.ConfirmPassword = newPassword;
        await _userRepository.UpdateAsync(user);
    }

    public async Task UpdateUserPasswordByUserEmailAsync(string email, string newPassword)
    {
        var user = await GetUserByEmailAsync(email);
        await UpdateUserPasswordAsync(user, newPassword);
    }

    public async Task DeleteUserByUserEmailAsync(string email)
    {
        var user = await GetUserByEmailAsync(email);
        await _userRepository.DeleteAsync(user);
    }

    public async Task UpdateUserNameAsync(User user, string userName)
    {
        user.UserName = userName;
        await _userRepository.UpdateAsync(user);
    }

    public async Task UpdateUserNameByUserEmailAsync(string email, string userName)
    {
        var user = await GetUserByEmailAsync(email);
        await UpdateUserNameAsync(user, userName);
    }

    public async Task<User> GetUserByUserNameAsync(string userName)
    {
        var user = await _applicationContext.Users.FirstOrDefaultAsync(user => user.UserName == userName);
        return user;
    }
}