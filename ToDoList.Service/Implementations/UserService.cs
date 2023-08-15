using System.Runtime.CompilerServices;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Enums;
using ToDoList.Domain.ErrorEntities;
using ToDoList.Infrastructure.Interfaces;
using ToDoList.Service.Interfaces;

namespace ToDoList.Service.Implementations;

public class UserService : IUserService
{
    private readonly IRepositoryManager _repositoryManager;
    public UserService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public void ChangeUserActive(long userId)
    {
        var user = GetUser(userId, trackChanges: true);
        user.Active = !user.Active;
        _repositoryManager.Save();
    }
    public void ChangeUserActive(string email)
    {
        var user = GetUserByEmail(email, trackChanges: true);
        user.Active = !user.Active;
        _repositoryManager.Save();
    }
    public void DeleteUser(User user)
    {
        _repositoryManager.UserRepository.DeleteUser(user);
        _repositoryManager.Save();
    }
    public void DeleteUserByEmail(string email)
    {
        var user = _repositoryManager.UserRepository.GetUserByEmail(email, trackChanges:true)
        ?? throw new NotFoundException($"User with email {email} not found.");
        _repositoryManager.UserRepository.DeleteUser(user);
        _repositoryManager.Save();
    }
    public void DeleteUserById(long userId)
    {
        var user = _repositoryManager.UserRepository.GetUser(userId, trackChanges: true) 
        ?? throw new NotFoundException($"User with ID {userId} not found.");
        _repositoryManager.UserRepository.DeleteUser(user);
        _repositoryManager.Save();
    }
    public IEnumerable<User> GetAllUsers(bool trackChanges) =>
    _repositoryManager.UserRepository.GetAllUsers(trackChanges);
    public IEnumerable<User> GetAllUsersByRole(Role role, bool trackChanges) =>
    _repositoryManager.UserRepository.GetAllUsersByRole(role, trackChanges);
    public User GetUser(long userId, bool trackChanges)
    {
        var user = _repositoryManager.UserRepository.GetUser(userId, trackChanges) 
        ?? throw new NotFoundException($"User with ID {userId} not found.");
        return user;
    }
    public User GetUserByEmail(string email, bool trackChanges)
    {
        var user = _repositoryManager.UserRepository.GetUserByEmail(email, trackChanges) 
        ?? throw new NotFoundException($"User with email {email} not found.");
        return user;
    }
    public User GetUserByName(string userName, bool trackChanges)
    {
        var user = _repositoryManager.UserRepository.GetUserByEmail(userName, trackChanges) 
        ?? throw new NotFoundException($"User with email {userName} not found.");
        return user;
    }
    public void UpdateUser(User user) => 
    _repositoryManager.UserRepository.UpdateUser(user);
    public void UpdateUserName(long userId, string userName)
    {
        var user = _repositoryManager.UserRepository.GetUser(userId, trackChanges:true) 
        ?? throw new NotFoundException($"User with ID {userId} not found.");
        user.UserName = userName;
        _repositoryManager.Save();
    }
    public void UpdateUserName(string email, string userName)
    {
        var user = _repositoryManager.UserRepository.GetUserByEmail(email, trackChanges:true)
        ?? throw new NotFoundException($"User with email {email} not found.");
        user.UserName = userName;
        _repositoryManager.Save();
    }
    public void UpdateUserPassword(long userId, string password)
    {
        var user = _repositoryManager.UserRepository.GetUser(userId, trackChanges:true) 
        ?? throw new NotFoundException($"User with ID {userId} not found.");
        user.Password = password;
        user.ConfirmPassword = password;
        _repositoryManager.Save();
    }
    public void UpdateUserPassword(string email, string password)
    {
        var user = _repositoryManager.UserRepository.GetUserByEmail(email, trackChanges:true)
        ?? throw new NotFoundException($"User with email {email} not found.");
        user.Password = password;
        user.ConfirmPassword = password;
        _repositoryManager.Save();
    }
    public void UpdateUserRole(long userId, Role role)
    {
        var user = _repositoryManager.UserRepository.GetUser(userId, trackChanges:true) 
        ?? throw new NotFoundException($"User with ID {userId} not found.");
        user.Role = role;
        _repositoryManager.Save();
    }
    public void UpdateUserRole(string email, Role role)
    {
        var user = _repositoryManager.UserRepository.GetUserByEmail(email, trackChanges:true)
        ?? throw new NotFoundException($"User with email {email} not found.");
        user.Role = role;
        _repositoryManager.Save();
    }
    public bool UserEmailExists(string email) =>
    _repositoryManager.UserRepository.UserEmailExists(email);
    public bool UserExists(long userId) => 
    _repositoryManager.UserRepository.UserExists(userId);
    public bool UserIsActive(long userId)
    {
        var user = GetUser(userId, trackChanges: false);
        return user.Active;
    }
    public bool UserIsActive(string email)
    {
        var user = GetUserByEmail(email, trackChanges: false);
        return user.Active;
    }
    public bool UserNameExists(string userName) =>
    _repositoryManager.UserRepository.UserNameExists(userName);
    public bool UserNameOrEmailExists(string emailOrUserName) =>
    _repositoryManager.UserRepository.UserNameOrEmailExists(emailOrUserName);
}