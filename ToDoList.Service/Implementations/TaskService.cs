using ToDoList.Domain.Entities;
using ToDoList.Domain.Enums;
using ToDoList.Infrastructure.Interfaces;
using ToDoList.Service.Interfaces;

namespace ToDoList.Service.Implementations;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public IQueryable<TaskEntity> SortTaskBySortStateCategory(IQueryable<TaskEntity> tasks, SortState sortStateCategory)
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

    public async Task UpdateTaskStatus(long taskId, Status newStatus)
    {
        var task = await _taskRepository.SelectAsync(taskId);
        task.Status = newStatus;
        await _taskRepository.UpdateAsync(task);
    }
}