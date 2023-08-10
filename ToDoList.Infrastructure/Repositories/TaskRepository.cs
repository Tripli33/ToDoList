using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entities;
using ToDoList.Infrastructure.Interfaces;

namespace ToDoList.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly ApplicationContext _applicationContext;

    public TaskRepository(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public async Task CreateAsync(TaskEntity entity)
    {
        await _applicationContext.Tasks.AddAsync(entity);
        await _applicationContext.SaveChangesAsync();
    }

    public async Task CreateUserTaskAsync(TaskEntity entity, string userEmail)
    {
        var user = await _applicationContext.Users.FirstOrDefaultAsync(user => user.Email == userEmail);
        user.Tasks.Add(entity);
        await _applicationContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(TaskEntity entity)
    {
        _applicationContext.Remove(entity);
        await _applicationContext.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(long entityId)
    {
        var task = await _applicationContext.Tasks.FindAsync(entityId);
        _applicationContext.Remove(task);
        await _applicationContext.SaveChangesAsync();
    }

    public IQueryable<TaskEntity> GetAllTasks()
    {
        return _applicationContext.Tasks;
    }

    public IQueryable<TaskEntity> GetUserTasksByEmail(string email)
    {
        var tasks = _applicationContext.Tasks.Where(t => t.User.Email == email);
        return tasks;
    }

    public async Task<TaskEntity> SelectAsync(long entityId)
    {
        var task = await _applicationContext.Tasks.FindAsync(entityId);
        return task;
    }

    public async Task UpdateAsync(TaskEntity entity)
    {
        _applicationContext.Tasks.Update(entity);
        await _applicationContext.SaveChangesAsync();
    }

    public async Task UpdateUserTaskAsync(TaskEntity entity, string userEmail)
    {
        var user = await _applicationContext.Users.FirstOrDefaultAsync(user => user.Email == userEmail);
        var task = user.Tasks.FirstOrDefault(task => task.Id == entity.Id);
        user.Tasks.Remove(task);
        user.Tasks.Add(entity);
        await _applicationContext.SaveChangesAsync();
    }
}