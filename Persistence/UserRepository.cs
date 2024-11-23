using Core.InterfaceContracts;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Persistence;

public class UserRepository: IUserStore
{
    private readonly MainDbContext _context; 
    public UserRepository(MainDbContext context)
    {
        _context = context;
    }
    public async Task<User> Add(User user)
    {
        //TODO: figure out weather db validation is supposed to be here
        
        // TODO: Dedicate weather this is the way to validate property's length
        // Validate fullName length and phoneNumberLength 
        
        bool registrationAllowed = !_context.Users.Any(user => user.Email == user.Email);//TODO: figure out weather this is db validation or not
        if (!registrationAllowed)
        {
            // TODO: Determine how to handle this case (the idea is to return 409 here or maybe write a
            // custom error handler that will throw "This email is already taken")
            return null;
        }
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public Task<User> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public User GetByCreds(string email, string password)//TODO: figure out weather this has to be Task<User> or not
    {
        //find item with corresponding email, check passwords for match
        var user = _context.Users.First(user => user.Email == email);// TODO: figure out weather we need to catch exception here
        if (user.Password == password)
        {
            return user;
        }
        // TODO: Determine how to handle this case 
        return null;
    }
}