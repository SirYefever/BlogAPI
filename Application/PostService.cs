using Core.InterfaceContracts;
using Core.Models;
using Core.ServiceContracts;

namespace Application;

public class PostService: IPostService
{
    private readonly IPostRepository _postRepository;
    public PostService(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public Task<Post[]> GetAvailabePosts()
    {
        throw new NotImplementedException();
    }

    public async Task<Post> CreatePost(Post post)
    {
        var createdPost = await _postRepository.Add(post);
        return createdPost;
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