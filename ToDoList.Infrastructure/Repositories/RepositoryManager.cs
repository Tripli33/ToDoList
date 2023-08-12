using ToDoList.Infrastructure.Interfaces;

namespace ToDoList.Infrastructure.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly ApplicationContext _appContext;
    private readonly Lazy<ITaskRepository> _taskRepository;
    private readonly Lazy<IUserRepository> _userRepository;

    public RepositoryManager(ApplicationContext appContext)
    {
        _appContext = appContext;
        _userRepository = new Lazy<IUserRepository>(() => new UserRepository(appContext));
        _taskRepository = new Lazy<ITaskRepository>(() => new TaskRepository(appContext));
    }

    public IUserRepository UserRepository => _userRepository.Value;
    public ITaskRepository TaskRepository => _taskRepository.Value;

    public void Save()
    {
        _appContext.SaveChanges();
    }
}