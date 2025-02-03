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
}