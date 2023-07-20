using System.Security.Claims;
using ToDoList.Domain.Entities;
using ToDoList.Domain.ViewModels;

namespace ToDoList.Service.Interfaces;

public interface IAccountService
{
    ClaimsPrincipal CreateClaimsPrincipal(List<Claim> claims, string authenticationType);
    Task<ClaimsPrincipal> CreateUserWithClaimsPrincipal(User user, List<Claim> claims, string authenticationType);
    Task<bool> VerifyUser(UserViewModel userViewModel);
}