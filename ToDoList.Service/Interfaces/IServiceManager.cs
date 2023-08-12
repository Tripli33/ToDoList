namespace ToDoList.Service.Interfaces;

public interface IServiceManager
{
    public IUserService UserService { get; }   
    public ITaskService TaskService { get; }
    public IAccountService AccountService { get; }
}