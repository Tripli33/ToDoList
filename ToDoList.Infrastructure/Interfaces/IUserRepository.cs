using ToDoList.Domain.Entities;

namespace ToDoList.Infrastructure.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User> GetUserByEmailAsync(string email);
    Task<User> GetUserByUserNameAsync(string userName);
    Task<User> GetUserByEmailOrUserNameAsync(string emailOrUserName);
}