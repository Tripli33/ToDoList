using System.Data.SqlTypes;
using ToDoList.Domain.Enums;

namespace ToDoList.Domain.ViewModels;

public class TaskSortHeaderViewModel
{
    public SortState TitleSort { get; set; }
    public SortState DeadLineSort { get; set; }
    public SortState PrioritySort { get; set; }
    public SortState StatusSort { get; set; }
    public SortState Current { get; set; }

    public TaskSortHeaderViewModel(SortState sortOrder)
    {
        TitleSort = SortState.TitleAsc;
        DeadLineSort = SortState.DeadLineAsc;
        PrioritySort = SortState.PriorityAsc;
        StatusSort = SortState.StatusAsc;

        Current = sortOrder switch {
            SortState.TitleAsc => TitleSort = SortState.TitleDesc,
            SortState.TitleDesc => TitleSort = SortState.TitleAsc,
            SortState.DeadLineAsc => DeadLineSort = SortState.DeadLineDesc,
            SortState.DeadLineDesc => DeadLineSort = SortState.DeadLineAsc,
            SortState.PriorityAsc => PrioritySort = SortState.PriorityDesc,
            SortState.PriorityDesc => PrioritySort = SortState.PriorityAsc,
            SortState.StatusAsc => StatusSort = SortState.StatusDesc,
            _ => StatusSort = SortState.StatusAsc
        };
    }
}