using Core.Models;

namespace Core.InterfaceContracts;

public interface IUserCommunityRepository
{
    Task<UserCommunity> CreateAsync(UserCommunity userCommunity);
    Task<List<Community>> GetAllAsync();
    Task<List<Community>> GetCommunitiesOfUserAsync(Guid userId);
    Task<List<User>> GetCommunitySubscribersAsync(Guid communityId);
}