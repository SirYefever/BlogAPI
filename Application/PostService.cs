using Core.Models;
using Core.ServiceContracts;

namespace Application;

public class PostService: IPostService
{
    public Task<Post[]> GetAvailabePosts()
    {
        throw new NotImplementedException();
    }

    public Task<Post> CreatePost(Post post)
    {
        throw new NotImplementedException();
    }

    public Task<Post> GetPostById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task LikePost(Guid postId)
    {
        throw new NotImplementedException();
    }

    public Task UnlikePost(Guid postId)
    {
        throw new NotImplementedException();
    }
}