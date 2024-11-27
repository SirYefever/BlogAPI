using API.Dto;
using Core.Models;

namespace Core.InterfaceContracts;

public interface IPostRepository
{
    Task<Post> Add(Post post);
    Task<Post> Get(Guid id);
    List<Post> GetAvailabePosts(PostListRequest request);
    Task AddLike(Guid postId, Guid userId);
    Task RemoveLike(Guid postId, Guid userId);
}
