using ToDoList.Domain.Enums;
namespace ToDoList.Service.Interfaces;

public interface ITaskService
{
    Task UpdateTaskStatus(long taskId, Status newStatus);
}