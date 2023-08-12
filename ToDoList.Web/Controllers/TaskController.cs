using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Enums;
using ToDoList.Domain.ViewModels;
using ToDoList.Service.Interfaces;

namespace ToDoList.Web.Controllers;

[Authorize]
public class TaskController : Controller
{
    private readonly IServiceManager _serviceManager;
    public int pageSize = 10;

    public TaskController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public IActionResult Index(SortState sortOrder = SortState.DeadLineAsc, int taskPage = 1)
    {
        var email = User.Identity?.Name ?? string.Empty;
        var allTasks = _serviceManager.TaskService.GetAllTasks(email, false);
        allTasks = _serviceManager.TaskService.SortTaskBySortStateCategory(allTasks, sortOrder);
        return View(new TaskListViewModel
        {
            Tasks = allTasks
                .Skip((taskPage - 1) * pageSize)
                .Take(pageSize),
            TaskSortHeaderInfo = new TaskSortHeaderInfo(sortOrder),
            PagingInfo = new PagingInfo
            {
                TotalItems = allTasks.Count(),
                ItemsPerPage = pageSize,
                CurrentPage = taskPage
            }
        });
    }

    public ActionResult UpdateTask(long taskId)
    {
        var email = User.Identity?.Name ?? string.Empty;
        var task = _serviceManager.TaskService.GetTask(email, taskId, true);
        return View("TaskForm", task);
    }

    public IActionResult DeleteTask(long taskId)
    {
        var email = User.Identity?.Name ?? string.Empty;
        _serviceManager.TaskService.DeleteTask(email, taskId);
        return RedirectToAction("Index");
    }

    public IActionResult StartTask(long taskId)
    {
        var email = User.Identity?.Name ?? string.Empty;
        _serviceManager.TaskService.UpdateTaskStatus(email, taskId, Status.InProgress);
        return RedirectToAction("Index");
    }

    public IActionResult CloseTask(long taskId)
    {
        var email = User.Identity?.Name ?? string.Empty;
        _serviceManager.TaskService.UpdateTaskStatus(email, taskId, Status.Completed);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public ViewResult TaskForm()
    {
        return View();
    }

    [HttpPost]
    public IActionResult TaskForm(TaskEntity task)
    {
        var email = User.Identity?.Name ?? string.Empty;
        if (!ModelState.IsValid) return View();
        if (task.Id != 0)
            _serviceManager.TaskService.UpdateTask(task);
        else
            _serviceManager.TaskService.CreateTask(email, task);
        return RedirectToAction("Index");
    }
}