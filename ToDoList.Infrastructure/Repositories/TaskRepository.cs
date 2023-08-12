using ToDoList.Domain.Entities;
using ToDoList.Infrastructure.Interfaces;

namespace ToDoList.Infrastructure.Repositories;

public class TaskRepository : BaseRepository<TaskEntity>, ITaskRepository
{
    public TaskRepository(ApplicationContext appContext) : base(appContext)
    {
    }

    public void CreateTask(TaskEntity task)
    {
        Create(task);
    }

    public void DeleteTask(TaskEntity task)
    {
        Delete(task);
    }

    public IEnumerable<TaskEntity> GetAllTasks(long userId, bool trackChanges)
    {
        return FindByCondition(t => t.Id.Equals(userId), trackChanges).ToList();
    }

    public IEnumerable<TaskEntity> GetAllTasks(string email, bool trackChanges)
    {
        return FindByCondition(t => t.User!.Email.Equals(email), trackChanges).ToList();
    }

    public TaskEntity GetTask(long userId, long taskId, bool trackChanges)
    {
        return FindByCondition(t => t.UserId.Equals(userId) && t.Id.Equals(taskId), trackChanges).SingleOrDefault();
    }

    public TaskEntity GetTask(string email, long taskId, bool trackChanges)
    {
        return FindByCondition(t => t.User!.Email.Equals(email) && t.Id.Equals(taskId), trackChanges).SingleOrDefault();
    }

    public void UpdateTask(TaskEntity task)
    {
        Update(task);
    }
}