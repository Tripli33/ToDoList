using Microsoft.AspNetCore.Mvc;
using ToDoList.Infrastructure.Interfaces;
using ToDoList.Domain.Enums;

namespace ToDoList.Web.Controllers;

public class HomeController : Controller
{
    private readonly ITaskRepository _taskRepository;

    public HomeController(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
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
        var task = await _taskRepository.SelectAsync(taskId);
        task.Status = Status.InProgress;
        await _taskRepository.UpdateAsync(task);
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> CloseTask(long taskId)
    {
        var task = await _taskRepository.SelectAsync(taskId);
        task.Status = Status.Completed;
        await _taskRepository.UpdateAsync(task);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public ViewResult TaskForm()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> TaskForm(Domain.Entities.Task task)
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