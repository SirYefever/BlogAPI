using Core.InterfaceContracts;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class PostLikeRepository: IPostLikeRepository
{
    private readonly MainDbContext _context;

    public PostLikeRepository(MainDbContext context)
    {
        _context = context;
    }

    public async Task<List<PostLike>> GetAllAsync()
    {
        return await _context.PostLike.ToListAsync();
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

    public async Task<int> GetLikeCountByPostIdAsync(Guid postId)
    {
        var postLikeCount = await _context.PostLike.Where(p => p.PostId == postId).CountAsync();
        return postLikeCount;
    }

    public async Task<int> GetLikeCountByUserIdAsync(Guid userId)
    {
        var likesRecievedByUser = await _context.PostLike.Where(pl => pl.PostAuthorId == userId).CountAsync();
        return likesRecievedByUser;
    }
}