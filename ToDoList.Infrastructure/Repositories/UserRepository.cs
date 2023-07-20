using ToDoList.Domain.Entities;
using ToDoList.Infrastructure.Interfaces;
using System.Threading.Tasks;

namespace ToDoList.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationContext _applicationContext;

    public UserRepository(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public async Task CreateAsync(User entity)
    {
        await _applicationContext.Users.AddAsync(entity);
        await _applicationContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(User entity)
    {
        _applicationContext.Remove(entity);
        await _applicationContext.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(long entityId)
    {
        var user = await _applicationContext.Users.FindAsync(entityId);
        _applicationContext.Remove(user);
        await _applicationContext.SaveChangesAsync();
    }

    public async Task<User> SelectAsync(long entityId)
    {
        return await _applicationContext.Users.FindAsync(entityId);
    }

    public async Task UpdateAsync(User entity)
    {
        _applicationContext.Users.Update(entity);
        await _applicationContext.SaveChangesAsync();
    }
}