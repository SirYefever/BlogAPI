using Core.Models;

namespace Core.InterfaceContracts;

public interface ICommunityPostRepository
{
    Task<CommunityPost> CreateAsync(CommunityPost communityPost);
}