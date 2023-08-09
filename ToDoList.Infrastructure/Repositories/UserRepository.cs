using ToDoList.Domain.Entities;
using ToDoList.Domain.Enums;
using ToDoList.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

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

    public IQueryable<User> GetAllUsers()
    {
        return _applicationContext.Users;
    }

    public IQueryable<User> GetAllUserWithRoleUser()
    {
        return _applicationContext.Users.Where(u => u.Role == Role.User);
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        var user = await _applicationContext.Users.FirstOrDefaultAsync(user => user.Email == email);
        return user;
    }

    public async Task<User> GetUserByEmailOrUserNameAsync(string emailOrUserName)
    {
        var user = await _applicationContext.Users.FirstOrDefaultAsync(user => user.Email == emailOrUserName
        || user.UserName == emailOrUserName);
        return user;
    }

    public async Task<User> GetUserByUserNameAsync(string userName)
    {
        var user = await _applicationContext.Users.FirstOrDefaultAsync(user => user.UserName == userName);
        return user;
    }

    public async Task<User> SelectAsync(long entityId)
    {
        var user = await _applicationContext.Users.FindAsync(entityId);
        return user;
    }

    public async Task UpdateAsync(User entity)
    {
        _applicationContext.Users.Update(entity);
        await _applicationContext.SaveChangesAsync();
    }
}