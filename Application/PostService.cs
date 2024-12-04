using API.Dto;
using Core.InterfaceContracts;
using Core.Models;
using Core.ServiceContracts;

namespace Application;

public class PostService: IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserCommunityRepository _userCommunityRepository;
    private readonly ICommunityPostRepository _communityPostRepository;
    private readonly IPostLikeRepository _postLikeRepository;
    public PostService(IPostRepository postRepository, IUserRepository userRepository, IUserCommunityRepository userCommunityRepository, ICommunityPostRepository communityPostRepository, IPostLikeRepository postLikeRepository)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
        _userCommunityRepository = userCommunityRepository;
        _communityPostRepository = communityPostRepository;
        _postLikeRepository = postLikeRepository;
    }

    public async Task<List<Post>> GetAvailabePosts(PostListRequest request, Guid userId)
    {
        List<UserCommunity> curUserCommunities = null;
        if (request.OnlyMyCommunities)
        {
            curUserCommunities = await _userCommunityRepository.GetUserCommunitiesByUserIdAsync(userId);
        }

        var postLikes = await _postLikeRepository.GetAllAsync();
        var posts =  await _postRepository.GetAvailabePosts(request, userId, postLikes, curUserCommunities);
        return posts;
    }

    public async Task<Post> CreatePost(Post post)
    {
        var postCreator = await _userRepository.GetById(post.AuthorId);
        post.Author = postCreator.FullName;
        await _postRepository.Add(post);
        if (post.CommunityId.HasValue)
        {
            var communityPost = new CommunityPost((Guid)post.CommunityId, post.Id);
            await _communityPostRepository.CreateAsync(communityPost);
        }
        return post;
    }

    public async Task<Post> GetPostById(Guid id)
    {
        var post = await _postRepository.GetById(id);
        return post;
    }

    public async Task LikePost(Guid postId, Guid userId)
    {
        var post = await _postRepository.GetById(postId);
        var postLike = new PostLike(postId, userId, post.AuthorId);
        await _postLikeRepository.AddAsync(postLike);
    }

    public async Task UnlikePost(Guid postId, Guid userId)
    {
        await _postLikeRepository.DeleteAsync(postId, userId);
    }
}