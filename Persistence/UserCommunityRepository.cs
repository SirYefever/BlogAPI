using Core.InterfaceContracts;
using Core.Models;
using Infrastructure.Exceptions;
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
        if (!await _context.Communities.AnyAsync(c=>c.Id == userCommunity.CommunityId))
            throw new KeyNotFoundException("Community id=" + userCommunity.CommunityId + " not found in database.");
        
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
        if (!await _context.Communities.AnyAsync(c=>c.Id == communityId))
            throw new KeyNotFoundException("Community id=" + communityId + " not found in database.");
        
        var userCommunityToDelete = await _context.UserCommunity.FirstOrDefaultAsync(uc =>
            uc.UserId == userId && uc.CommunityId == communityId);
        
        if (userCommunityToDelete == null)
            throw new KeyNotFoundException("User id=" + userId + " does not belong to community id=" + communityId + ".");
        
        _context.UserCommunity.Remove(userCommunityToDelete);
        await _context.SaveChangesAsync();
    }

    public async Task<int> GetSubscriberCountById(Guid communityId)
    {
        var result = await _context.UserCommunity.CountAsync(x => x.CommunityId == communityId);
        return result;
    }

    public async Task<CommunityRole> GetHighestRoleOfUserInCommunity(Guid communityId, Guid userId)
    {
        if (!await _context.Communities.AnyAsync(x => x.Id == communityId))
            throw new KeyNotFoundException("Community id=" + communityId + " not found in database");
        
        var userCommunity =
            await _context.UserCommunity.FirstOrDefaultAsync(uc => uc.UserId == userId && uc.CommunityId == communityId);
            
        if (userCommunity == null)
            throw new KeyNotFoundException("User id=" + userId.ToString() + "does not belong to community id=" + communityId.ToString());
            
        return userCommunity.HighestRole;
    }

    public async Task ConfirmUserBelongsToClosedCommunity(Guid communityId, Guid userId)
    {
        var community = await _context.Communities.FirstOrDefaultAsync(c => c.Id == communityId);
        
        if (community == null)
            throw new KeyNotFoundException("Community id=" + communityId.ToString() + " not found in database.");
        
        if (! await _context.UserCommunity.AnyAsync(uc => uc.UserId == userId && uc.CommunityId == communityId) && 
            community.IsClosed)
            throw new ForbiddenException("User id=" + userId.ToString() + "does not belong to closed community id="+ communityId.ToString());
    }

    public async Task<bool> IsUserInCommunity(Guid communityId, Guid userId)
    {
        
        var community = await _context.Communities.FirstOrDefaultAsync(c => c.Id == communityId);
        
        if (community == null)
            throw new KeyNotFoundException("Community id=" + communityId.ToString() + " not found in database.");

        if (!await _context.UserCommunity.AnyAsync(uc => uc.UserId == userId && uc.CommunityId == communityId))
            return false;
        
        return true;
    }
}