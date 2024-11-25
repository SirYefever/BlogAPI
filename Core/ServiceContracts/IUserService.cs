using Core.Models;

namespace Core.ServiceContracts;

public interface IUserService
{
   Task<User> GetUserById(Guid userId); 
   Task<User> CreateUser(User user); 
   Task<User> UpdateUser(User userToBeUpdated, User updatedUser); 
   Task<User> DeleteUser(User user); 
   Task<string> Login(string email, string password); 
}