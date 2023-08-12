using ToDoList.Domain.Entities;
using ToDoList.Domain.Enums;

namespace ToDoList.Infrastructure.Interfaces;

public interface IUserRepository
{
    IEnumerable<User> GetAllUsers(bool trackChanges);
    IEnumerable<User> GetAllUsersByRole(Role role, bool trackChanges);
    User GetUser(long userId, bool trackChanges);
    User GetUserByEmail(string email, bool trackChanges);
    User GetUserByName(string userName, bool trackChanges);
    void CreateUser(User user);
    void UpdateUser(User user);
    void DeleteUser(User user);
    bool UserExists(long userId);
    bool UserEmailExists(string email);
    bool UserNameExists(string userName);
    bool UserNameOrEmailExists(string emailOrUserName);
}