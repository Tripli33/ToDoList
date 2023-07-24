using Microsoft.AspNetCore.Mvc;
using ToDoList.Infrastructure.Interfaces;
using ToDoList.Domain.Enums;
using ToDoList.Service.Interfaces;
using ToDoList.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using ToDoList.Domain.ViewModels;

namespace ToDoList.Web.Controllers;
[Authorize]
public class TaskController : Controller
{
    private readonly ITaskRepository _taskRepository;
    private readonly ITaskService _taskService;
    public int pageSize = 10;

    public TaskController(ITaskRepository taskRepository, ITaskService taskService)
    {
        _taskRepository = taskRepository;
        _taskService = taskService;
    }
    public IActionResult Index(int taskPage = 1)
    {
        var allTasks = _taskRepository.GetAllTasks();
        return View(new TaskListViewModel(){
            Tasks = allTasks
            .Skip((taskPage - 1) * pageSize)
            .Take(pageSize),
            PagingInfo = new PagingInfo(){
                TotalItems = allTasks.Count(),
                ItemsPerPage = pageSize,
                CurrentPage = taskPage
            }
        });
    }
    public async Task<ActionResult> UpdateTask(long taskId)
    {
        var task = await _taskRepository.SelectAsync(taskId);
        return View("TaskForm", task);
    }
    public async Task<IActionResult> DeleteTask(long taskId)
    {
        await _taskRepository.DeleteByIdAsync(taskId);
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> StartTask(long taskId)
    {
        await _taskService.UpdateTaskStatus(taskId, Status.InProgress);
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> CloseTask(long taskId)
    {
        await _taskService.UpdateTaskStatus(taskId, Status.Completed);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public ViewResult TaskForm()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> TaskForm(TaskEntity task)
    {
        var userEmail = User.Identity.Name;
        if (!ModelState.IsValid)
        {
            return View();
        }
        if (task.Id != 0)
        {
            await _taskRepository.UpdateUserTaskAsync(task, userEmail);
        }
        else
        {
            await _taskRepository.CreateUserTaskAsync(task, userEmail);
        }
        return RedirectToAction("Index");
    }
}