using ToDoList.Domain.Entities;

namespace ToDoList.Infrastructure.Interfaces;

public interface ITaskRepository : IBaseRepository<TaskEntity>
{
    IQueryable<TaskEntity> GetAllTasks();
    Task CreateUserTaskAsync(TaskEntity entity, string userEmail);
    Task UpdateUserTaskAsync(TaskEntity entity, string userEmail);
}