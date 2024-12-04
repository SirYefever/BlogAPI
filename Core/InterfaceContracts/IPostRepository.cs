using API.Dto;
using Core.Models;

namespace Core.InterfaceContracts;

public interface IPostRepository
{
    Task<Post> Add(Post post);
    Task<Post> GetById(Guid id);
    Task<List<Post>> GetAvailabePosts(PostListRequest request, Guid userId, List<UserCommunity> curUserCommunities);
    Task Like(Guid postId);
    Task Unlike(Guid postId);
}
