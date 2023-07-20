using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entities;

namespace ToDoList.Infrastructure;

public class ApplicationContext : DbContext
{
    public DbSet<TaskEntity> Tasks { get; set; }
    public DbSet<User> Users { get; set; }
    public ApplicationContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }
}