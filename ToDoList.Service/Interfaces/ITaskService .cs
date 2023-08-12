using ToDoList.Domain.Entities;
using ToDoList.Domain.Enums;

namespace ToDoList.Service.Interfaces;

public interface ITaskService 
{
    void CreateTask(TaskEntity task);
    void CreateTask(long userId, TaskEntity task);
    void CreateTask(string email, TaskEntity task);
    TaskEntity GetTask(long userId, long taskId, bool trackChanges);
    TaskEntity GetTask(string email, long taskId, bool trackChanges);
    IEnumerable<TaskEntity> GetAllTasks(long userId, bool trackChanges);
    IEnumerable<TaskEntity> GetAllTasks(string email, bool trackChanges);
    void UpdateTask(TaskEntity task);
    void UpdateTaskStatus(TaskEntity task, Status status);
    void UpdateTaskStatus(long userId, long taskId, Status status);
    void UpdateTaskStatus(string email, long taskId, Status status);
    void DeleteTask(TaskEntity task);
    void DeleteTask(long userId, long taskId);
    void DeleteTask(string email, long taskId);
    IEnumerable<TaskEntity> SortTaskBySortStateCategory(IEnumerable<TaskEntity> tasks, SortState sortStateCategory);
}