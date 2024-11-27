using API.Dto;
using Core.Models;

namespace Core.ServiceContracts;

public interface IPostService
{
    public List<Post> GetAvailabePosts(PostListRequest request);
    public Task<Post> CreatePost(Post post);
    public Task<Post> GetPostById(Guid id);
    public Task LikePost(Guid postId);//TODO: figure out weather we need UserId for this
    public Task UnlikePost(Guid postId);//TODO: figure out weather we need UserId for thi
}