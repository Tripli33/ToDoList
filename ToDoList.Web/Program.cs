using ToDoList.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ToDoList.Infrastructure.Interfaces;
using ToDoList.Infrastructure.Repositories;
using ToDoList.Service.Interfaces;
using ToDoList.Service.Implementations;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => options.LoginPath = "/Account/Login");

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IAccountService, AccountService>();

var connectionString = builder.Configuration.GetConnectionString("MySQL");
builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseMySql(connectionString,
    new MySqlServerVersion(new Version(8, 0, 33)));
});

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// app.MapWhen(context => context.User.Identity.IsAuthenticated,
// app => app.UseEndpoints(endpoints => endpoints.MapControllerRoute(
//     name: "authPath",
//     pattern: "{controller=Task}/{action=Index}/{id?}"
// )));
app.MapControllerRoute("pagination",
"Tasks/Page{taskPage}",
new {Controller = "Task", action = "Index", taskPage = 1});
app.MapDefaultControllerRoute();

app.Run();
