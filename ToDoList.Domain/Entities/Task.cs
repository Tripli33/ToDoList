using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Domain.Enums;

namespace ToDoList.Domain.Entities;

public class Task
{
    public long Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public string? Description { get; set; }
    public DateTime DeadLine { get; set; } = DateTime.Today;
    public Priority Priority { get; set; }
    public Status Status { get; set; }
}