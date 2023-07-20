using System.Security.Claims;
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

    public async Task<bool> VerifyUser(UserViewModel userViewModel)
    {
        var user = await _applicationContext.Users.FirstOrDefaultAsync(user => user.Email == userViewModel.Email);
        return user is null || user.Password != userViewModel.Password;
    }
}