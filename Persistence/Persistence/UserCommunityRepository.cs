using Core.InterfaceContracts;
using Core.Models;

namespace Persistence.Persistence;

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

    public Task<List<Community>> GetCommunitiesOfUserAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<List<User>> GetCommunitySubscribersAsync(Guid communityId)
    {
        throw new NotImplementedException();
    }
}