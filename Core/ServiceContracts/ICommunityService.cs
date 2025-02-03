using API.Dto;
using Core.Models;

namespace Core.ServiceContracts;

public interface ICommunityService
{
    Task<Community> CreateCommunityAsync(Community community);
    Task<Community> GetCommunityById(Guid communityId);
    Task<List<Community>> GetCommunities();
    Task<List<Post>> GetPostsOfCommunity(CommunityPostListRequest request, Guid userId);
    Task<List<User>> GetCommunityAdmins(Guid communityId);
    public Task<int> GetSubscriberCountByCommunityId(Guid communityId);
    Task<int> GetPostQuantity(Guid communityId);
}