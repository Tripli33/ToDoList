using ToDoList.Domain.Entities;

namespace ToDoList.Infrastructure.Interfaces;

public interface ITaskRepository : IBaseRepository<TaskEntity>
{
    IQueryable<TaskEntity> GetAllTasks();
}