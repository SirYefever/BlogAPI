using Core.Models;

namespace Core.ServiceContracts;

public interface IUserService
{
   //TODO: implement 
   Task<User> GetUserById(Guid userId); 
   Task<User> CreateUser(User user); 
   Task<User> UpdateUser(User userToBeUpdated, User updatedUser); 
   Task<User> DeleteUser(User user); 
   User Login(string email, string password); 
}