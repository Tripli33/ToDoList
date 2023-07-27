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
    private readonly ApplicationContext _applicationContext;
    private readonly IUserRepository _userRepository;
    public AccountController(IAccountService accountService, ApplicationContext applicationContext, IUserRepository userRepository)
    {
        _accountService = accountService;
        _applicationContext = applicationContext;
        _userRepository = userRepository;
    }

    public ViewResult Login() => View();
    [HttpPost]
    public async Task<IActionResult> Login(string? returnUrl, UserViewModel userViewModel)
    {
        if (await _accountService.VerifyUser(userViewModel)){
            return Unauthorized();
        }
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
    public ViewResult Profile(){
        var user = _applicationContext.Users.FirstOrDefault(user => user.Email == User.Identity.Name);
        return View(new UserProfileViewModel() {UserName = user.UserName});
    }
    public async Task<IActionResult> ChangeUserName(string userName){
        var user = await _applicationContext.Users.FirstOrDefaultAsync(user => user.Email == User.Identity.Name);
        user.UserName = userName;
        await _userRepository.UpdateAsync(user);
        return RedirectToAction("Profile");
    }
    public async Task<IActionResult> UpdatePassword(UserPasswordViewModel userPasswordViewModel){
        var user = await _applicationContext.Users.FirstOrDefaultAsync(user => user.Email == User.Identity.Name);
        user.Password = userPasswordViewModel.NewPassword;
        user.ConfirmPassword = userPasswordViewModel.ConfirmNewPassword;
        await _userRepository.UpdateAsync(user);
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
    public async Task<IActionResult> DeleteAccount(){
        var user = await _applicationContext.Users.FirstOrDefaultAsync(user => user.Email == User.Identity.Name);
        await _userRepository.DeleteByIdAsync(user.UserId);
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
    [AcceptVerbs("Post", "Get")]
    public async Task<IActionResult> CheckUserName(string userName){
        var user = await _applicationContext.Users.FirstOrDefaultAsync(user => user.UserName == userName);
        return user is not null ? Json(false) : Json(true);
    }
    [AcceptVerbs("Post", "Get")]
    public async Task<IActionResult> CheckEmail(string email){
        var user = await _applicationContext.Users.FirstOrDefaultAsync(user => user.Email == email);
        return user is not null ? Json(false) : Json(true);
    }

}