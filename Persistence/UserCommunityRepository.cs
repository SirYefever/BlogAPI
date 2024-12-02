using Core.InterfaceContracts;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class UserCommunityRepository: IUserCommunityRepository
{
    private readonly MainDbContext _context;

    public UserCommunityRepository(MainDbContext context)
    {
        _context = context;
    }

    public async Task<UserCommunity> CreateAsync(UserCommunity userCommunity)
    {
        _context.UserCommunity.Add(userCommunity);
        await _context.SaveChangesAsync();
        return userCommunity;
    }

    public Task<List<Community>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<List<UserCommunity>> GetUserCommunitiesByUserIdAsync(Guid userId)
    {
        var userCommunityList = await _context.UserCommunity
            .Where(uc => uc.UserId == userId)
            .ToListAsync();
        return userCommunityList;
    }

    public async Task DeleteByIds(Guid communityId, Guid userId)
    {
        var userCommunityToDelete = await _context.UserCommunity.FirstAsync(uc =>
            uc.UserId == userId && uc.CommunityId == communityId);
        _context.UserCommunity.Remove(userCommunityToDelete);
        await _context.SaveChangesAsync();
    }

    public Task<List<User>> GetCommunitySubscribersAsync(Guid communityId)
    {
        throw new NotImplementedException();
    }

    public async Task<int> GetSubscriberCountById(Guid communityId)
    {
        var result = await _context.UserCommunity.CountAsync(x => x.CommunityId == communityId);
        return result;
    }
}