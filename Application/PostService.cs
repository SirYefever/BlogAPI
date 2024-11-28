using API.Dto;
using Core.InterfaceContracts;
using Core.Models;
using Core.ServiceContracts;

namespace Application;

public class PostService: IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    public PostService(IPostRepository postRepository, IUserRepository userRepository)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
    }

    public List<Post> GetAvailabePosts(PostListRequest request)
    {
        var posts =  _postRepository.GetAvailabePosts(request);
        return posts;
    }

    public async Task<Post> CreatePost(Post post)
    {
        var postCreator = await _userRepository.GetById(post.AuthorId);//TODO: figure out weather services are
            //supposed to reference not corresponding repositories(IUserRepository) directly or do they need to
            //reference service (IUserService)?
        post.Author = postCreator.FullName;
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