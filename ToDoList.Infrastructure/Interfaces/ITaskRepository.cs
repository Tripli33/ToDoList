using ToDoList.Domain.Entities;

namespace ToDoList.Infrastructure.Interfaces;

public interface ITaskRepository
{
    IEnumerable<TaskEntity> GetAllTasks(long userId, bool trackChanges);
    IEnumerable<TaskEntity> GetAllTasks(string email, bool trackChanges);
    TaskEntity GetTask(long userId, long taskId, bool trackChanges);
    TaskEntity GetTask(string email, long taskId, bool trackChanges);
    void CreateTask(TaskEntity task);
    void UpdateTask(TaskEntity task);
    void DeleteTask(TaskEntity task);
}