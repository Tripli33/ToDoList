using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using ToDoList.Domain.Entities;
using ToDoList.Domain.ErrorEntities;
using ToDoList.Domain.ViewModels;
using ToDoList.Infrastructure.Interfaces;
using ToDoList.Service.Interfaces;

namespace ToDoList.Service.Implementations;

public class AccountService : IAccountService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public AccountService(IRepositoryManager repositoryManager, IHttpContextAccessor httpContextAccessor)
    {
        _repositoryManager = repositoryManager;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task SignInWithDefaultPrincipalAsync(User user, string authenticationScheme)
    {
        if (!_repositoryManager.UserRepository.UserExists(user.UserId)){
            _repositoryManager.UserRepository.CreateUser(user);
            _repositoryManager.Save();
        }
        var claims = new List<Claim> { 
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString()) };
        var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        await _httpContextAccessor.HttpContext.SignInAsync(authenticationScheme, claimsPrincipal);
    }
    public async Task SignInWithDefaultPrincipalAsync(UserLoginViewModel userLoginViewModel, string authenticationScheme)
    {
        var emailOrUserName = userLoginViewModel.EmailOrUserName;
        var user = emailOrUserName.Contains('@')
        ? _repositoryManager.UserRepository.GetUserByEmail(emailOrUserName, trackChanges: false)
        : _repositoryManager.UserRepository.GetUserByName(emailOrUserName, trackChanges:false)
        ?? throw new NotFoundException($"User with email or name {emailOrUserName} not found.");
        await SignInWithDefaultPrincipalAsync(user, authenticationScheme);
    }
    public async Task SignOutAsync(string authenticationScheme) => 
    await _httpContextAccessor.HttpContext.SignOutAsync(authenticationScheme);
    public bool VerifyUserLoginViewModel(UserLoginViewModel userLoginViewModel)
    {
        var emailOrUserName = userLoginViewModel.EmailOrUserName;
        if (string.IsNullOrWhiteSpace(emailOrUserName)) return false;
        var user = emailOrUserName.Contains('@')
        ? _repositoryManager.UserRepository.GetUserByEmail(emailOrUserName, trackChanges: false)
        : _repositoryManager.UserRepository.GetUserByName(emailOrUserName, trackChanges:false);
        return user is null || user.Password != userLoginViewModel.Password;        
    }
}
