using Core.Models;

namespace Core.InterfaceContracts;

public interface IUserRepository
{
    Task<User> Add(User user);
    Task<User> GetById(Guid id);
    Task<User> GetByEmail(string email);
    Task<string> GetTokenByEmail(string email);
    Task<User> Update(Guid userToUpdateId, User newUser);
    Task Logout(Guid userId);
}