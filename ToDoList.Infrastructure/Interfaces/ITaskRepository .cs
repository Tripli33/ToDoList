using ToDoList.Domain.Entities;

namespace ToDoList.Infrastructure.Interfaces;

public interface ITaskRepository : IBaseRepository<TaskEntity>
{
    IQueryable<TaskEntity> GetAllTasks();
    IQueryable<TaskEntity> GetUserTasksByEmail(string email);
    Task CreateUserTaskAsync(TaskEntity entity, string userEmail);
    Task UpdateUserTaskAsync(TaskEntity entity, string userEmail);
}