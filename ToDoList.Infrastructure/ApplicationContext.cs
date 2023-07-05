using Microsoft.EntityFrameworkCore;

namespace ToDoList.Infrastructure;

public class ApplicationContext : DbContext
{
    public DbSet<Domain.Entities.Task> Tasks { get; set; }
    public ApplicationContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }
}