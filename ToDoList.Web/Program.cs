using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ToDoList.Infrastructure;
using ToDoList.Infrastructure.Interfaces;
using ToDoList.Infrastructure.Repositories;
using ToDoList.Service.Implementations;
using ToDoList.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Error";
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
    });

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<IServiceManager, ServiceManager>();

var connectionString = builder.Configuration.GetConnectionString("MySQL");
builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseMySql(connectionString,
        new MySqlServerVersion(new Version(8, 0, 33)));
});

var app = builder.Build();

// app.Environment.EnvironmentName = "Production";

if (!app.Environment.IsDevelopment())
    app.UseExceptionHandler("/Error");
else
    app.UseDeveloperExceptionPage();

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
    new { Controller = "Task", action = "Index", taskPage = 1 });
app.MapDefaultControllerRoute();
app.MapRazorPages();
app.Run();