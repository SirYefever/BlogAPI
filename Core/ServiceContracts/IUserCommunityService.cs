using Core.Models;

namespace Core.ServiceContracts;

public interface IUserCommunityService
{
    public Task AddUserToTheCommunity(Guid communityId, Guid userId, CommunityRole communityRole);
    public Task UnsubscribeUserToCommunityAsync(Guid communityId, Guid userId);
    public  Task<List<UserCommunity>> GetUserCommunitiesByUserIdAsync(Guid userId);
    public Task<CommunityRole> GetHighestRoleOfUserInCommunity(Guid communityId, Guid userId);
    public Task<bool> IsUserInTheCommunity(Guid communityId, Guid userId);
}