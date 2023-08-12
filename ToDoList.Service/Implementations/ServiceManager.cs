using Microsoft.AspNetCore.Http;
using ToDoList.Infrastructure.Interfaces;
using ToDoList.Service.Interfaces;

namespace ToDoList.Service.Implementations;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IUserService> _userService;
    private readonly Lazy<ITaskService> _taskService;
    private readonly Lazy<IAccountService> _accountService;
    public ServiceManager(IRepositoryManager repositoryManager, IHttpContextAccessor httpContextAccessor)
    {
        _userService = new Lazy<IUserService>(() => new UserService(repositoryManager));
        _taskService = new Lazy<ITaskService>(() => new TaskService(repositoryManager));
        _accountService = new Lazy<IAccountService>(() => new AccountService(repositoryManager, httpContextAccessor));
    }
    public IUserService UserService => _userService.Value;
    public ITaskService TaskService => _taskService.Value;
    public IAccountService AccountService => _accountService.Value;
}