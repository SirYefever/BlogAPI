using Core.Models;

namespace Core.ServiceContracts;

public interface IPostService
{
    Task<List<Post>> GetAvailabePosts(PostListRequest request, Guid userId);
    Task<Post> CreatePost(Post post, List<Guid> tagGuids);
    Task CreatePersonal(Post post, List<Guid> tagGuids);
    Task<Post> GetPostById(Guid id, Guid userId);
    Task LikePost(Guid postId, Guid userId);
    Task UnlikePost(Guid postId, Guid userId);
    Task ParseTags(string text);
}