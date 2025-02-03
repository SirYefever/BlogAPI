using Core.Models;

namespace Core.InterfaceContracts;

public interface IUserCommunityRepository
{
    Task<UserCommunity> CreateAsync(UserCommunity userCommunity);
    Task<List<UserCommunity>> GetUserCommunitiesByUserIdAsync(Guid userId);
    Task DeleteByIds(Guid communityId, Guid userId);
    public Task<int> GetSubscriberCountById(Guid communityId);
    public Task<CommunityRole> GetHighestRoleOfUserInCommunity(Guid communityId, Guid userId);
    Task<List<UserCommunity>> GetCommunityAdmins(Guid communityId);
    public Task ConfirmUserBelongsToClosedCommunity(Guid communityId, Guid userId);
    public Task<bool> IsUserInCommunity(Guid communityId, Guid userId);
}