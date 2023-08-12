using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Enums;
using ToDoList.Domain.ViewModels;
using ToDoList.Service.Interfaces;

namespace ToDoList.Web.Controllers;

public class AccountController : Controller
{
    private readonly IServiceManager _serviceManager;

    public AccountController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public ViewResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string? returnUrl, UserLoginViewModel userLoginViewModel)
    {
        if (!ModelState.IsValid) return View();
        if (_serviceManager.AccountService.VerifyUserLoginViewModel(userLoginViewModel))
            return Unauthorized();
        await _serviceManager.AccountService
            .SignInWithDefaultPrincipalAsync(userLoginViewModel, CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect(returnUrl ?? "/Task/Index");
    }

    public async Task<IActionResult> Logout()
    {
        await _serviceManager.AccountService
            .SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    public ViewResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(string? returnUrl, User user)
    {
        if (!ModelState.IsValid) return View();
        await _serviceManager.AccountService
            .SignInWithDefaultPrincipalAsync(user, CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect(returnUrl ?? "/Task/Index");
    }

    public ViewResult Profile()
    {
        var email = User.Identity?.Name ?? string.Empty;
        var user = _serviceManager.UserService.GetUserByEmail(email, false);
        return View(new UserProfileViewModel { UserName = user.UserName });
    }

    public IActionResult ChangeUserName(string userName)
    {
        if (!ModelState.IsValid) return View("Profile");
        var email = User.Identity?.Name ?? string.Empty;
        _serviceManager.UserService.UpdateUserName(email, userName);
        return RedirectToAction("Profile");
    }

    public async Task<IActionResult> UpdatePassword(UserPasswordViewModel userPasswordViewModel)
    {
        if (!ModelState.IsValid) return View("Profile");
        var email = User.Identity?.Name ?? string.Empty;
        _serviceManager.UserService.UpdateUserPassword(email, userPasswordViewModel.NewPassword);
        await _serviceManager.AccountService.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }

    public async Task<IActionResult> DeleteAccount()
    {
        var email = User.Identity?.Name ?? string.Empty;
        _serviceManager.UserService.DeleteUserByEmail(email);
        await _serviceManager.AccountService.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }

    [Authorize(Roles = "Admin")]
    public IActionResult AdminPanel()
    {
        var users = _serviceManager.UserService.GetAllUsersByRole(Role.User, false);
        return View(users);
    }

    public IActionResult UpdateUserRole(long userId)
    {
        _serviceManager.UserService.UpdateUserRole(userId, Role.Admin);
        return RedirectToAction("AdminPanel");
    }

    public IActionResult DeleteUser(long userId)
    {
        _serviceManager.UserService.DeleteUserById(userId);
        return RedirectToAction("AdminPanel");
    }

    [AcceptVerbs("Post", "Get")]
    public bool CheckUserName(string userName)
    {
        return !_serviceManager.UserService.UserNameExists(userName);
    }

    [AcceptVerbs("Post", "Get")]
    public bool CheckEmail(string email)
    {
        return !_serviceManager.UserService.UserEmailExists(email);
    }

    [AcceptVerbs("Post", "Get")]
    public bool CheckOldPassword(UserPasswordViewModel userPasswordViewModel)
    {
        var email = User.Identity?.Name ?? string.Empty;
        return !_serviceManager.AccountService.VerifyUserLoginViewModel(new UserLoginViewModel
            { EmailOrUserName = email, Password = userPasswordViewModel.OldPassword });
    }
}