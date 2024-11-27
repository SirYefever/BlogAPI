using Core.InterfaceContracts;
using Core.Models;
using Core.ServiceContracts;

namespace Application;

public class CommunityService: ICommunityService
{
    private readonly ICommunityRepository _communityRepository;

    public CommunityService(ICommunityRepository communityRepository)
    {
        _communityRepository = communityRepository;
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
}