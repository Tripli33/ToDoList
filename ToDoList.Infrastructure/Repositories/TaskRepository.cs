using Microsoft.EntityFrameworkCore;
using ToDoList.Infrastructure.Interfaces;

namespace ToDoList.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly ApplicationContext _applicationContext;

    public TaskRepository(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public async Task CreateAsync(Domain.Entities.Task entity)
    {
        await _applicationContext.Tasks.AddAsync(entity);
        await _applicationContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Domain.Entities.Task entity)
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

    public IQueryable<Domain.Entities.Task> GetAllTasks()
    {
        return _applicationContext.Tasks;
    }

    public async Task<Domain.Entities.Task> SelectAsync(long entityId)
    {
        return await _applicationContext.Tasks.FindAsync(entityId);
    }

    public async Task UpdateAsync(Domain.Entities.Task entity)
    {
        _applicationContext.Tasks.Update(entity);
        await _applicationContext.SaveChangesAsync();
    }

}