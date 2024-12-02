using Core.Models;

namespace Core.InterfaceContracts;

public interface ICommunityPostRepository
{
    Task<CommunityPost> CreateAsync(CommunityPost communityPost);
    Task<CommunityPost> GetByIdAsync(Guid id);
    Task DeleteAsync(Guid communityId, Guid postId);
}