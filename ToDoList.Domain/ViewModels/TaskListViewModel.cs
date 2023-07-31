using ToDoList.Domain.Entities;

namespace ToDoList.Domain.ViewModels;

public class TaskListViewModel
{
    public IEnumerable<TaskEntity> Tasks { get; set; } = Enumerable.Empty<TaskEntity>();
    public TaskSortHeaderViewModel TaskSortHeaderViewModel { get; set; }
    public PagingInfo PagingInfo { get; set; } = new();
}