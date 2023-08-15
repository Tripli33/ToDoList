using System.Runtime.CompilerServices;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Enums;

namespace ToDoList.Service.Interfaces;

public interface IUserService 
{
    IEnumerable<User> GetAllUsers(bool trackChanges);
    IEnumerable<User> GetAllUsersByRole(Role role, bool trackChanges);
    User GetUser(long userId, bool trackChanges);
    User GetUserByEmail(string email, bool trackChanges);
    User GetUserByName(string userName, bool trackChanges);
    void UpdateUser(User user);
    void UpdateUserName(long userId, string userName);
    void UpdateUserName(string email, string userName);
    void UpdateUserPassword(long userId, string password);
    void UpdateUserPassword(string email, string password);
    void UpdateUserRole(long userId, Role role);
    void UpdateUserRole(string email, Role role);
    bool UserExists(long userId);
    bool UserEmailExists(string email);
    bool UserNameExists(string userName);
    bool UserNameOrEmailExists(string emailOrUserName);
    void DeleteUser(User user);
    void DeleteUserById(long userId);
    void DeleteUserByEmail(string email);
    void ChangeUserActive(long userId);
    void ChangeUserActive(string email);
    bool UserIsActive(long userId);
    bool UserIsActive(string email);
}