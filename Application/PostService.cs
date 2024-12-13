using System.Text.RegularExpressions;
using Core.InterfaceContracts;
using Core.Models;
using Core.ServiceContracts;

namespace Application;

public class PostService : IPostService
{
    private readonly ICommunityPostRepository _communityPostRepository;
    private readonly IGarRepository _garRepository;
    private readonly IPostLikeRepository _postLikeRepository;
    private readonly IPostRepository _postRepository;
    private readonly IPostTagRepository _postTagRepository;
    private readonly ITagRepository _tagRepository;
    private readonly IUserCommunityRepository _userCommunityRepository;
    private readonly IUserRepository _userRepository;

    public PostService(IPostRepository postRepository, IUserRepository userRepository,
        IUserCommunityRepository userCommunityRepository, ICommunityPostRepository communityPostRepository,
        IPostLikeRepository postLikeRepository, IGarRepository garRepository, ITagRepository tagRepository,
        IPostTagRepository postTagRepository)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
        _userCommunityRepository = userCommunityRepository;
        _communityPostRepository = communityPostRepository;
        _postLikeRepository = postLikeRepository;
        _garRepository = garRepository;
        _tagRepository = tagRepository;
        _postTagRepository = postTagRepository;
    }

    public async Task<List<Post>> GetAvailabePosts(PostListRequest request, Guid userId)
    {
        List<UserCommunity> curUserCommunities = null;
        if (request.OnlyMyCommunities)
            curUserCommunities = await _userCommunityRepository.GetUserCommunitiesByUserIdAsync(userId);

        var postLikes = await _postLikeRepository.GetAllAsync();
        var posts = await _postRepository.GetAvailabePosts(request, userId, postLikes, curUserCommunities);
        return posts;
    }

    public async Task<Post> CreatePost(Post post, List<Guid> tagGuids)
    {
        await _userCommunityRepository.ConfirmUserBelongsToClosedCommunity((Guid)post.CommunityId, post.AuthorId);

        await _postTagRepository.AddListOfPostTags(tagGuids, post.Id);

        await ParseTags(post.Title);
        await ParseTags(post.Description);

        await _postRepository.Add(post);
        var communityPost = new CommunityPost((Guid)post.CommunityId, post.Id);
        await _communityPostRepository.CreateAsync(communityPost);
        return post;
    }

    public async Task CreatePersonal(Post post, List<Guid> tagGuids)
    {
        if (post.AdressId.HasValue)
            await _garRepository.ConfirmAddressIsReal((Guid)post.AdressId);

        await _postTagRepository.AddListOfPostTags(tagGuids, post.Id);

        await ParseTags(post.Title);
        await ParseTags(post.Description);

        await _postRepository.AddPersonal(post);
    }

    public async Task ParseTags(string text)
    {
        var regex = @"#[^ #\n]+";
        var matches = Regex.Matches(text, regex);
        var tags = new List<Tag>();
        foreach (Match match in matches)
        {
            var newTag = new Tag(match.Value.Substring(1, match.Value.Length - 1));
            tags.Add(newTag);
            await _tagRepository.Add(newTag);
        }
    }

    public async Task<Post> GetPostById(Guid id, Guid userId)
    {
        return await _postRepository.GetById(id, userId);
    }

    public async Task LikePost(Guid postId, Guid userId)
    {
        var post = await _postRepository.GetById(postId, userId);
        var postLike = new PostLike(postId, userId, post.AuthorId);
        await _postLikeRepository.AddAsync(postLike);
    }

    public async Task UnlikePost(Guid postId, Guid userId)
    {
        await _postLikeRepository.DeleteAsync(postId, userId);
    }
}