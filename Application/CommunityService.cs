using API.Dto;
using Core.InterfaceContracts;
using Core.Models;
using Core.ServiceContracts;

namespace Application;

public class CommunityService : ICommunityService
{
    private readonly ICommunityRepository _communityRepository;
    private readonly IPostLikeRepository _postLikeRepository;
    private readonly IUserCommunityRepository _userCommunityRepository;
    private readonly IUserRepository _userRepository;

    public CommunityService(ICommunityRepository communityRepository, IUserCommunityRepository userCommunityRepository,
        IPostLikeRepository postLikeRepository, IUserRepository userRepository)
    {
        _communityRepository = communityRepository;
        _userCommunityRepository = userCommunityRepository;
        _postLikeRepository = postLikeRepository;
        _userRepository = userRepository;
    }

    public async Task<Community> CreateCommunityAsync(Community community)
    {
        await _communityRepository.Add(community);
        return community;
    }

    public async Task<Community> GetCommunityById(Guid communityId)
    {
        return await _communityRepository.GetById(communityId);
    }

    public async Task<List<User>> GetCommunityAdmins(Guid communityId)
    {
        var userCommunityList = await _userCommunityRepository.GetCommunityAdmins(communityId);
        var adminsNotAwaited = userCommunityList.Select(uc => _userRepository.GetById(uc.UserId));
        List<User> result = (await Task.WhenAll(adminsNotAwaited)).ToList();
        return result;
    }

    public async Task<int> GetSubscriberCountByCommunityId(Guid communityId)
    {
        var subscriberCount = await _userCommunityRepository.GetSubscriberCountById(communityId);
        return subscriberCount;
    }

    public async Task<int> GetPostQuantity(Guid communityId)
    {
        return await _communityRepository.GetPostQuantity(communityId);
    }

    public async Task<List<Community>> GetCommunities()
    {
        return await _communityRepository.GetAll();
    }

    public async Task<List<Post>> GetPostsOfCommunity(CommunityPostListRequest request, Guid userId)
    {
        var postLikes = await _postLikeRepository.GetAllAsync();
        return await _communityRepository.GetPostsOfCommunity(request, postLikes, userId);
    }
}