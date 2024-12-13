using Core.InterfaceContracts;
using Core.Models;

namespace Persistence;

public class CommunityPostRepository : ICommunityPostRepository
{
    private readonly MainDbContext _context;

    public CommunityPostRepository(MainDbContext context)
    {
        _context = context;
    }

    public async Task<CommunityPost> CreateAsync(CommunityPost communityPost)
    {
        _context.CommunityPost.Add(communityPost);
        await _context.SaveChangesAsync();
        return communityPost;
    }

    public Task<CommunityPost> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid communityId, Guid postId)
    {
        throw new NotImplementedException();
    }
}