using Core.InterfaceContracts;
using Core.Models;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class UserRepository : IUserRepository
{
    private readonly MainDbContext _context;

    public UserRepository(MainDbContext context)
    {
        _context = context;
    }

    public async Task<User> Add(User user)
    {
        if (await _context.Users.AnyAsync(item => item.Email == user.Email))
            throw new BadRequestException("Username " + user.Email + " is already taken.");

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> GetById(Guid id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(item => item.Id == id);
        if (user == null)
            throw new KeyNotFoundException("User id=" + id + " not found in database.");

        return await _context.Users.FirstOrDefaultAsync(item => item.Id == id);
    }

    public async Task<User> GetByEmail(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
        if (user == null)
            throw new KeyNotFoundException("User email=" + email + "not found in database.");

        return user;
    }

    public async Task<string> GetTokenByEmail(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
        return user.Token;
    }

    public async Task<User> Update(Guid userToUpdateId, User newUser)
    {
        var user = _context.Users.FirstOrDefault(user => user.Id == userToUpdateId);
        if (user == null)
            throw new KeyNotFoundException("User id=" + userToUpdateId + " not found in database.");

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