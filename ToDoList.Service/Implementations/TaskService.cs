using ToDoList.Domain.Entities;
using ToDoList.Domain.Enums;
using ToDoList.Domain.ErrorEntities;
using ToDoList.Infrastructure.Interfaces;
using ToDoList.Service.Interfaces;

namespace ToDoList.Service.Implementations;

public class TaskService : ITaskService
{
    private readonly IRepositoryManager _repositoryManager;
    public TaskService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }
    public void CreateTask(TaskEntity task)
    {
        _repositoryManager.TaskRepository.CreateTask(task);
        _repositoryManager.Save();
    }
    public void CreateTask(long userId, TaskEntity task)
    {
        var user = _repositoryManager.UserRepository.GetUser(userId, trackChanges:true) 
        ?? throw new NotFoundException($"User with ID {userId} not found.");
        user.Tasks.Add(task);
        _repositoryManager.Save();
    }
    public void CreateTask(string email, TaskEntity task)
    {
        var user = _repositoryManager.UserRepository.GetUserByEmail(email, trackChanges:true) 
        ?? throw new NotFoundException($"User with email {email} not found.");
        user.Tasks.Add(task);
        _repositoryManager.Save();
    }
    public void DeleteTask(TaskEntity task)
    {
        _repositoryManager.TaskRepository.DeleteTask(task);
        _repositoryManager.Save();
    }
    public void DeleteTask(long userId, long taskId)
    {
        var user = _repositoryManager.UserRepository.GetUser(userId, trackChanges:true)
        ?? throw new NotFoundException($"User with ID {userId} not found.");
        var task = _repositoryManager.TaskRepository.GetTask(userId, taskId, trackChanges:true)
        ?? throw new NotFoundException($"Task with ID {taskId} not found.");
        user.Tasks.Remove(task);
        _repositoryManager.Save();
    }
    public void DeleteTask(string email, long taskId)
    {
        var user = _repositoryManager.UserRepository.GetUserByEmail(email, trackChanges:true)
        ?? throw new NotFoundException($"User with email {email} not found.");
        var task = _repositoryManager.TaskRepository.GetTask(email, taskId, trackChanges:true)
        ?? throw new NotFoundException($"Task with ID {taskId} not found.");
        user.Tasks.Remove(task);
        _repositoryManager.Save();
    }
    public IEnumerable<TaskEntity> GetAllTasks(long userId, bool trackChanges)
    {
        var tasks = _repositoryManager.TaskRepository.GetAllTasks(userId, trackChanges)
        ?? throw new NotFoundException($"User with ID {userId} not found.");
        return tasks;
    }
    public IEnumerable<TaskEntity> GetAllTasks(string email, bool trackChanges)
    {
        var tasks = _repositoryManager.TaskRepository.GetAllTasks(email, trackChanges)
        ?? throw new NotFoundException($"User with email {email} not found.");
        return tasks;
    }
    public TaskEntity GetTask(long userId, long taskId, bool trackChanges)
    {
        if (!_repositoryManager.UserRepository.UserExists(userId))
        throw new NotFoundException($"User with ID {userId} not found.");
        var task = _repositoryManager.TaskRepository.GetTask(userId, taskId, trackChanges:true)
        ?? throw new NotFoundException($"Task with ID {taskId} not found.");
        return task;
    }
    public TaskEntity GetTask(string email, long taskId, bool trackChanges)
    {
        if (!_repositoryManager.UserRepository.UserEmailExists(email))
        throw new NotFoundException($"User with email {email} not found.");
        var task = _repositoryManager.TaskRepository.GetTask(email, taskId, trackChanges:true)
        ?? throw new NotFoundException($"Task with ID {taskId} not found.");
        return task;
    }
    public void UpdateTask(TaskEntity task)
    {
        _repositoryManager.TaskRepository.UpdateTask(task);
        _repositoryManager.Save();
    }
    public void UpdateTaskStatus(TaskEntity task, Status status)
    {
        task.Status = status;
        _repositoryManager.TaskRepository.UpdateTask(task);
        _repositoryManager.Save();
    }
    public void UpdateTaskStatus(long userId, long taskId, Status status)
    {
        var task = GetTask(userId, taskId, trackChanges:true);
        task.Status = status;
        _repositoryManager.TaskRepository.UpdateTask(task);
        _repositoryManager.Save();
    }
    public void UpdateTaskStatus(string email, long taskId, Status status)
    {
        var task = GetTask(email, taskId, trackChanges:true);
        task.Status = status;
        _repositoryManager.TaskRepository.UpdateTask(task);
        _repositoryManager.Save();
    }
    public IEnumerable<TaskEntity> SortTaskBySortStateCategory(IEnumerable<TaskEntity> tasks, SortState sortStateCategory)
    {
        return sortStateCategory switch
        {
            SortState.TitleAsc => tasks.OrderBy(task => task.Title),
            SortState.TitleDesc => tasks.OrderByDescending(task => task.Title),
            SortState.DeadLineAsc => tasks.OrderBy(task => task.DeadLine),
            SortState.DeadLineDesc => tasks.OrderByDescending(task => task.DeadLine),
            SortState.PriorityAsc => tasks.OrderBy(task => task.Priority),
            SortState.PriorityDesc => tasks.OrderByDescending(task => task.Priority),
            SortState.StatusAsc => tasks.OrderBy(task => task.Status),
            _ => tasks.OrderByDescending(task => task.Status)
        };
    }
}
