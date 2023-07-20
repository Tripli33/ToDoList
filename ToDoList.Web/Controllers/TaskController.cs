using Microsoft.AspNetCore.Mvc;
using ToDoList.Infrastructure.Interfaces;
using ToDoList.Domain.Enums;
using ToDoList.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using ToDoList.Domain.Entities;

namespace ToDoList.Web.Controllers;
[Authorize]
public class TaskController : Controller
{
    private readonly ITaskRepository _taskRepository;
    private readonly ITaskService _taskService;

    public TaskController(ITaskRepository taskRepository, ITaskService taskService)
    {
        _taskRepository = taskRepository;
        _taskService = taskService;
    }

    public IActionResult Index()
    {
        var allTasks = _taskRepository.GetAllTasks();
        return View(allTasks);
    }
    public async Task<ActionResult> UpdateTask(long taskId){
        var task = await _taskRepository.SelectAsync(taskId);
        return View("TaskForm", task);
    }
    public async Task<IActionResult> DeleteTask(long taskId){
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
        if (!ModelState.IsValid){
            return View();
        }
        if (task.Id != 0){
            await _taskRepository.UpdateAsync(task);
        }
        else{
            await _taskRepository.CreateAsync(task);
        }
        return RedirectToAction("Index");
    }
}