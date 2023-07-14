using ToDoList.Domain.Enums;
using ToDoList.Infrastructure.Interfaces;
using ToDoList.Infrastructure.Repositories;
using ToDoList.Service.Interfaces;

namespace ToDoList.Service.Implementations;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task UpdateTaskStatus(long taskId, Status newStatus)
    {
        var task = await _taskRepository.SelectAsync(taskId);
        task.Status = newStatus;
        await _taskRepository.UpdateAsync(task);
    }
}