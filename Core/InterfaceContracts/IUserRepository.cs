using Core.Models;

namespace Core.InterfaceContracts;

public interface IUserRepository
{
    Task<User> Add(User user);
    Task<User> GetById(Guid id);
    // User GetByCreds(string email, string password);//TODO: figure out weather it's better to pass UserCredetials here or string email and string password
    Task<User> GetByEmail(string email);
    Task<string> GetTokenByEmail(string email);
    Task<User> Update(Guid userToUpdateId, User newUser);
    Task Logout(Guid userId);
}