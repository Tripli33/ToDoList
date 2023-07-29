using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entities;
using ToDoList.Domain.ViewModels;
using ToDoList.Infrastructure;
using ToDoList.Infrastructure.Interfaces;
using ToDoList.Service.Interfaces;

namespace ToDoList.Web.Controllers;

public class AccountController : Controller
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public ViewResult Login() => View();
    [HttpPost]
    public async Task<IActionResult> Login(string? returnUrl, UserViewModel userViewModel)
    {
        if (await _accountService.VerifyUserViewModelAsync(userViewModel)){
            return Unauthorized();
        }

        if (!userViewModel.EmailOrUserName.Contains('@')){
            var user = await _accountService.GetUserByUserNameAsync(userViewModel.EmailOrUserName);
            userViewModel.EmailOrUserName = user.Email;
        }

        var claims = new List<Claim> { new Claim(ClaimTypes.Name, userViewModel.EmailOrUserName) };
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
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Email) };
        var claimsPrincipal = await _accountService.CreateUserWithClaimsPrincipal(user, claims, "Cookies");
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
        return Redirect(returnUrl ?? "/Task/Index");
    }
    public async Task<ViewResult> Profile(){
        var email = User.Identity.Name;
        var user = await _accountService.GetUserByEmailAsync(email);
        return View(new UserProfileViewModel() {UserName = user.UserName});
    }
    public async Task<IActionResult> ChangeUserName(string userName){
        if (!ModelState.IsValid)
        {
            return View("Profile");
        }
        var email = User.Identity.Name;
        await _accountService.UpdateUserNameByUserEmailAsync(email, userName);
        return RedirectToAction("Profile");
    }
    public async Task<IActionResult> UpdatePassword(UserPasswordViewModel userPasswordViewModel){
        if (!ModelState.IsValid)
        {
            return View("Profile");
        }
        var email = User.Identity.Name;
        await _accountService.UpdateUserPasswordByUserEmailAsync(email, userPasswordViewModel.NewPassword);
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
    public async Task<IActionResult> DeleteAccount(){
        var email = User.Identity.Name;
        await _accountService.DeleteUserByUserEmailAsync(email);
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
    [AcceptVerbs("Post", "Get")]
    public async Task<bool> CheckUserName(string userName){
        var user = await _accountService.GetUserByUserNameAsync(userName);
        return user is null;
    }
    [AcceptVerbs("Post", "Get")]
    public async Task<bool> CheckEmail(string email){
        var user = await _accountService.GetUserByEmailAsync(email);
        return user is null;
    }
    [AcceptVerbs("Post", "Get")]
    public async Task<bool> CheckOldPassword(string oldPassword){
        var email = User.Identity.Name;
        return !await _accountService.VerifyUserViewModelAsync(new UserViewModel() {EmailOrUserName = email, Password = oldPassword});
    }
}