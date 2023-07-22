using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Web.Controllers;

public class HomeController : Controller
{
    public ViewResult Index() => View();
}