using Core.Models;

namespace Core.ServiceContracts;

public interface IUserService
{
    Task<User> GetUserById(Guid userId);
    Task<User> CreateUser(User user);
    Task UpdateUser(Guid userToBeUpdatedId, User updatedUser);
    Task<string> Login(string email, string password);
    Task Logout(Guid userId);
}