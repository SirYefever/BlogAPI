using API.Dto;
using Core.InterfaceContracts;
using Core.Models;
using Core.ServiceContracts;

namespace Application;

public class CommunityService: ICommunityService
{
    private readonly ICommunityRepository _communityRepository;
    private readonly IUserCommunityRepository _userCommunityRepository;

    public CommunityService(ICommunityRepository communityRepository, IUserCommunityRepository userCommunityRepository)
    {
        _communityRepository = communityRepository;
        _userCommunityRepository = userCommunityRepository;
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

    public async Task<int> GetSubscriberCountByCommunityId(Guid communityId)
    {
        var subscriberCount = await _userCommunityRepository.GetSubscriberCountById(communityId);
        return subscriberCount;
    }

    public async Task<List<Community>> GetCommunities()
    {
        return await _communityRepository.GetAll();
    }

    public Task<Community> UpdateCommunity(Guid communityToUpdateId, Community newCommunity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCommunity(Guid communityToDeleteId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Post>> GetPostsOfCommunity(CommunityPostListRequest request)
    {
        return await _communityRepository.GetPostsOfCommunity(request);
    }
}