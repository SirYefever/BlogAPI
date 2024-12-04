using Core.InterfaceContracts;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Persistence;

public class UserRepository: IUserRepository
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
            throw new ("Registration with there credentials is not allowed.");
        }
        //TODO: figure out weather next to lines have to be wrapped with try catch
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> GetById(Guid id)
    {
        return await _context.Users.FirstOrDefaultAsync(item => item.Id == id);
    }

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

    public async Task<string> GetTokenByEmail(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
        return user.Token;
    }

    public async Task<User> Update(User userToUpdate, User newUser)
    {
        var user = _context.Users.FirstOrDefault(user => user.Id == userToUpdate.Id);
        _context.Entry(user).CurrentValues.SetValues(newUser);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task Logout(Guid userId)
    {
        var user = await _context.Users.FirstAsync(u => u.Id == userId);
        if (user != null)
        {
            user.Token = null;
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException("User not found.");
        }    
    }
}