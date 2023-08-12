namespace ToDoList.Infrastructure.Interfaces;

public interface IRepositoryManager
{
    public IUserRepository UserRepository { get; }
    public ITaskRepository TaskRepository { get; }
    void Save();
}