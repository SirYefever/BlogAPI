namespace Core.ServiceContracts;

public interface IUserCommunityService
{
    public Task SubscribeUserToCommunityAsync(Guid userId, Guid communityId);
    public Task UnsubscribeUserToCommunityAsync(Guid userId, Guid communityId);
}