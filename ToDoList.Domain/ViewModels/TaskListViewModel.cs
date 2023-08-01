using ToDoList.Domain.Entities;

namespace ToDoList.Domain.ViewModels;

public class TaskListViewModel
{
    public IEnumerable<TaskEntity> Tasks { get; set; } = Enumerable.Empty<TaskEntity>();
    public TaskSortHeaderInfo TaskSortHeaderInfo { get; set; } = new();
    public PagingInfo PagingInfo { get; set; } = new();
}