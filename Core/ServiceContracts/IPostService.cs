using API.Dto;
using Core.Models;

namespace Core.ServiceContracts;

public interface IPostService
{
    public Task<List<Post>> GetAvailabePosts(PostListRequest request, Guid userId);
    public Task<Post> CreatePost(Post post);
    public Task<Post> GetPostById(Guid id);
    public Task LikePost(Guid postId, Guid userId);
    public Task UnlikePost(Guid postId, Guid userId);//TODO: figure out weather we need UserId for thi
}