using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ToDoList.Infrastructure.Interfaces;

namespace ToDoList.Infrastructure.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly ApplicationContext _appContext;

    protected BaseRepository(ApplicationContext appContext)
    {
        _appContext = appContext;
    }

    public void Create(T entity)
    {
        _appContext.Set<T>().Add(entity);
    }

    public void Delete(T entity)
    {
        _appContext.Set<T>().Remove(entity);
    }

    public IQueryable<T> FindAll(bool trackChanges)
    {
        return !trackChanges ? _appContext.Set<T>().AsNoTracking() : _appContext.Set<T>();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
    {
        return !trackChanges
            ? _appContext.Set<T>().Where(expression).AsNoTracking()
            : _appContext.Set<T>().Where(expression);
    }

    public void Update(T entity)
    {
        _appContext.Set<T>().Update(entity);
    }
}