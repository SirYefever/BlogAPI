using Core.Models;

namespace Core.InterfaceContracts;

public interface IUserStore
{
    Task<User> Add(User user);
    Task<User> GetById(Guid id);
    User GetByCreds(string email, string password);//TODO: figure out weather it's better to pass UserCredetials here or string email and string password
}