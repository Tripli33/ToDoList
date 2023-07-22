using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Entities;
using ToDoList.Domain.ViewModels;
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
        if (await _accountService.VerifyUser(userViewModel)) Unauthorized();
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, userViewModel!.Email) };
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
}