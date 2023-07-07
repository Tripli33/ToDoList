using Microsoft.AspNetCore.Mvc;
using ToDoList.Infrastructure.Interfaces;

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
        await _taskRepository.CreateAsync(task);
        return RedirectToAction("Index");
    }
}