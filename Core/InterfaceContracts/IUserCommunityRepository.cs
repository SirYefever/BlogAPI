using Core.Models;

namespace Core.InterfaceContracts;

public interface IUserCommunityRepository
{
    Task<UserCommunity> CreateAsync(UserCommunity userCommunity);
    Task<List<Community>> GetAllAsync();
    Task<List<UserCommunity>> GetUserCommunitiesByUserIdAsync(Guid userId);
    Task DeleteByIds(Guid communityId, Guid userId);
}