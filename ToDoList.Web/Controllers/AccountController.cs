using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Enums;
using ToDoList.Domain.ViewModels;
using ToDoList.Infrastructure.Interfaces;
using ToDoList.Service.Interfaces;

namespace ToDoList.Web.Controllers;

public class AccountController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly IAccountService _accountService;

    public AccountController(IUserRepository userRepository, IAccountService accountService)
    {
        _userRepository = userRepository;
        _accountService = accountService;
    }

    public ViewResult Login() => View();
    [HttpPost]
    public async Task<IActionResult> Login(string? returnUrl, UserLoginViewModel userLoginViewModel)
    {
        if (await _accountService.VerifyUserLoginViewModelAsync(userLoginViewModel)){
            return Unauthorized();
        }

        var user = await _userRepository.GetUserByEmailOrUserNameAsync(userLoginViewModel.EmailOrUserName);

        var claims = new List<Claim> { 
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString()) };
        var claimsPrincipal = _accountService.CreateClaimsPrincipal(claims, "Cookies");
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
        return Redirect(returnUrl ?? "/Task/Index");
    }
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
    public ViewResult Register() => View();
    [HttpPost]
    public async Task<IActionResult> Register(string? returnUrl, User user)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        var claims = new List<Claim> { 
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString()) };
        var claimsPrincipal = await _accountService.CreateUserWithClaimsPrincipalAsync(user, claims, "Cookies");
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
        return Redirect(returnUrl ?? "/Task/Index");
    }
    public async Task<ViewResult> Profile(){
        var email = User.Identity?.Name ?? String.Empty;
        var user = await _userRepository.GetUserByEmailAsync(email);
        return View(new UserProfileViewModel() {UserName = user.UserName});
    }
    public async Task<IActionResult> ChangeUserName(string userName){
        if (!ModelState.IsValid)
        {
            return View("Profile");
        }
        var email = User.Identity?.Name ?? String.Empty;
        await _accountService.UpdateUserNameByEmailAsync(email, userName);
        return RedirectToAction("Profile");
    }
    public async Task<IActionResult> UpdatePassword(UserPasswordViewModel userPasswordViewModel){
        if (!ModelState.IsValid)
        {
            return View("Profile");
        }
        var email = User.Identity?.Name ?? String.Empty;
        await _accountService.UpdateUserPasswordByEmailAsync(email, userPasswordViewModel.NewPassword);
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
    public async Task<IActionResult> DeleteAccount(){
        var email = User.Identity?.Name ?? String.Empty;
        await _accountService.DeleteUserByEmailAsync(email);
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
    [Authorize(Roles = "Admin")]
    public IActionResult AdminPanel()
    {
        var users = _userRepository.GetAllUserWithRoleUser();
        return View(users);
    }
    public async Task<IActionResult> UpdateUserRole(long userId){
        await _accountService.UpdateUserRoleAsync(userId, Role.Admin);
        return RedirectToAction("AdminPanel");
    }
    public async Task<IActionResult> DeleteUser(long userId){
        await _userRepository.DeleteByIdAsync(userId);
        return RedirectToAction("AdminPanel");
    }
    [AcceptVerbs("Post", "Get")]
    public async Task<bool> CheckUserName(string userName){
        var user = await _userRepository.GetUserByUserNameAsync(userName);
        return user is null;
    }
    [AcceptVerbs("Post", "Get")]
    public async Task<bool> CheckEmail(string email){
        var user = await _userRepository.GetUserByEmailAsync(email);
        return user is null;
    }
    [AcceptVerbs("Post", "Get")]
    public async Task<bool> CheckOldPassword(UserPasswordViewModel userPasswordViewModel){
        var email = User.Identity?.Name ?? String.Empty;
        return !await _accountService.VerifyUserLoginViewModelAsync(new UserLoginViewModel()
        {EmailOrUserName = email, Password = userPasswordViewModel.OldPassword});
    }
}