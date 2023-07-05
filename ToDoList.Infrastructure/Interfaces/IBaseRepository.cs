namespace ToDoList.Infrastructure.Interfaces;

public interface IBaseRepository<T>
{
    Task CreateAsync(T entity);

    Task<T> SelectAsync(long entityId);

    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);
}