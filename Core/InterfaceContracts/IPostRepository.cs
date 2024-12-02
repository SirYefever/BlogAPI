using API.Dto;
using Core.Models;

namespace Core.InterfaceContracts;

public interface IPostRepository
{
    Task<Post> Add(Post post);
    Task<Post> Get(Guid id);
    Task<List<Post>> GetAvailabePosts(PostListRequest request, Guid userId, List<UserCommunity> curUserCommunities);
    Task AddLike(Guid postId, Guid userId);
    Task RemoveLike(Guid postId, Guid userId);
}
