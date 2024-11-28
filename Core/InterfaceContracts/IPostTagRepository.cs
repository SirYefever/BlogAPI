using Core.Models;

namespace Core.InterfaceContracts;

public interface IPostTagRepository
{
    
    // Task<UserCommunity> CreateAsync(UserCommunity userCommunity);
    // Task<List<Community>> GetAllAsync();
    // Task<List<Community>> GetCommunitiesOfUserAsync(Guid userId);
    // Task<List<User>> GetCommunitySubscribersAsync(Guid communityId);
    Task<PostTag> CreateAsync(PostTag postTag);
    Task<PostTag> GetAllAsync(Guid id);
    Task<PostTag> GetTagsOfPost(Guid postId);
    Task<PostTag> GetPostsByTagId(Guid tagId);
}