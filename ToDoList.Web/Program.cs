using ToDoList.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ToDoList.Infrastructure.Interfaces;
using ToDoList.Infrastructure.Repositories;
using ToDoList.Service.Interfaces;
using ToDoList.Service.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddScoped<ITaskRepository, TaskRepository>();

builder.Services.AddScoped<ITaskService, TaskService>();

var connectionString = builder.Configuration.GetConnectionString("MySQL");
builder.Services.AddDbContext<ApplicationContext>(options => {
    options.UseMySql(connectionString,
    new MySqlServerVersion(new Version(8, 0, 33)));
});

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapDefaultControllerRoute();

app.Run();
