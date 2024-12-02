using API.Dto;
using Core.Models;

namespace Core.ServiceContracts;

public interface ICommunityService
{
    Task<Community> CreateCommunityAsync(Community community);
    Task<Community> GetCommunityById(Guid communityId);
    Task<List<Community>> GetCommunities();
    Task<Community> UpdateCommunity(Guid communityToUpdateId, Community newCommunity);
    Task DeleteCommunity(Guid communityToDeleteId);
    Task<List<Post>> GetPostsOfCommunity(CommunityPostListRequest request);
    public Task<int> GetSubscriberCountByCommunityId(Guid communityId);
}