using Core.InterfaceContracts;
using Core.Models;
using Core.ServiceContracts;

namespace Application;

public class UserCommunityService: IUserCommunityService
{
    private readonly IUserCommunityRepository _userCommunityRepository;

    public UserCommunityService(IUserCommunityRepository userCommunityRepository)
    {
        _userCommunityRepository = userCommunityRepository;
    }

    public async Task AddUserToTheCommunity(Guid communityId, Guid userId, CommunityRole communityRole)
    {
        var userCommunity = new UserCommunity(communityId, userId, communityRole);
        await _userCommunityRepository.CreateAsync(userCommunity);
    }

    public async Task UnsubscribeUserToCommunityAsync(Guid communityId, Guid userId)
    {
        await _userCommunityRepository.DeleteByIds(communityId, userId);
    }

    public async Task<List<UserCommunity>> GetUserCommunitiesByUserIdAsync(Guid userId)
    {
        var userCommunityList = await _userCommunityRepository.GetUserCommunitiesByUserIdAsync(userId);
        return userCommunityList;
    }
}