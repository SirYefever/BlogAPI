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

    public async Task SubscribeUserToCommunityAsync(Guid userId, Guid communityId)
    {
        var userCommunity = new UserCommunity(userId, communityId);
        await _userCommunityRepository.CreateAsync(userCommunity);
    }

    public Task UnsubscribeUserToCommunityAsync(Guid userId, Guid communityId)
    {
        throw new NotImplementedException();
    }
}