using Core.InterfaceContracts;
using Core.Models;

namespace Persistence;

public class PostLikeRepository: IPostLikeRepository
{
    private readonly MainDbContext _context;

    public PostLikeRepository(MainDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(PostLike postLike)
    {
        _context.PostLike.Add(postLike);
        await _context.SaveChangesAsync();
    }

    public Task DeleteAsync(Guid postId, Guid userId)
    {
        var postLike = _context.PostLike.Find(postId, userId);
        if (postLike != null)
        {
            _context.PostLike.Remove(postLike);
            return _context.SaveChangesAsync();
        }
        else
        {
            throw new ArgumentException("Such like was not found.");
        }
    }
}