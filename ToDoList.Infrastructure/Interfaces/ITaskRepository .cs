namespace ToDoList.Infrastructure.Interfaces;

public interface ITaskRepository : IBaseRepository<Domain.Entities.Task>
{
    IQueryable<Domain.Entities.Task> GetAllTasks();
}