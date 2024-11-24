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
        
        // Validate fullName length and phoneNumberLength 
        
        bool registrationAllowed = !_context.Users.Any(item => item.Email == user.Email);//TODO: figure out weather this is db validation or not
        if (!registrationAllowed)
        {
            //TODO: throw exception... but in which way?
        }
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public Task<User> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    //TODO: figure out weather this function is needed or not.
    // public User GetByCreds(string email, string password)//TODO: figure out weather this has to be Task<User> or not
    // {
    //     //find item with corresponding email, check passwords for match
    //     var user = _context.Users.First(user => user.Email == email);// TODO: figure out weather we need to catch exception here
    //     if (user.Password == password)
    //     {
    //         return user;
    //     }
    //     // TODO: Determine how to handle this case 
    //     return null;
    // }

    public async Task<User> GetByEmail(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
        if (user == null)
        {
            //TODO: figure out how to handle this case
            throw new NullReferenceException("User not found");
        }
        return user;
    }
}