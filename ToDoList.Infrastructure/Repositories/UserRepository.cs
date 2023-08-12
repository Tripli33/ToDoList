using ToDoList.Domain.Entities;
using ToDoList.Domain.Enums;
using ToDoList.Infrastructure.Interfaces;

namespace ToDoList.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ApplicationContext appContext) : base(appContext)
    {
    }

    public void CreateUser(User user)
    {
        Create(user);
    }

    public void DeleteUser(User user)
    {
        Delete(user);
    }

    public IEnumerable<User> GetAllUsers(bool trackChanges)
    {
        return FindAll(trackChanges).ToList();
    }

    public IEnumerable<User> GetAllUsersByRole(Role role, bool trackChanges)
    {
        return FindByCondition(u => u.Role.Equals(role), trackChanges).ToList();
    }

    public User GetUser(long userId, bool trackChanges)
    {
        return FindByCondition(u => u.UserId.Equals(userId), trackChanges).SingleOrDefault();
    }

    public User GetUserByEmail(string email, bool trackChanges)
    {
        return FindByCondition(u => u.Email.Equals(email), trackChanges).SingleOrDefault();
    }

    public User GetUserByName(string userName, bool trackChanges)
    {
        return FindByCondition(u => u.UserName.Equals(userName), trackChanges).SingleOrDefault();
    }

    public void UpdateUser(User user)
    {
        Update(user);
    }

    public bool UserEmailExists(string email)
    {
        return FindAll(false).Any(u => u.Email.Equals(email));
    }

    public bool UserExists(long userId)
    {
        return FindAll(false).Any(u => u.UserId.Equals(userId));
    }

    public bool UserNameExists(string userName)
    {
        return FindAll(false).Any(u => u.UserName.Equals(userName));
    }

    public bool UserNameOrEmailExists(string emailOrUserName)
    {
        return FindAll(false).Any(u => u.UserName.Equals(emailOrUserName)
                                       || u.Email.Equals(emailOrUserName));
    }
}