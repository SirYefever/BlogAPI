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
        if (!await _context.Posts.AnyAsync(x => x.Id == postLike.PostId)) 
            throw new KeyNotFoundException("Post id=" + postLike.PostId + " not found in database.");
        
        _context.PostLike.Add(postLike);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid postId, Guid userId)
    {
        var postLike = await _context.PostLike.FirstOrDefaultAsync(x=> x.PostId == postId && x.UserWhoLikedId == userId);
        if (postLike == null)
            throw new KeyNotFoundException("There is no like for this post by user.");
        
        _context.PostLike.Remove(postLike);
        await _context.SaveChangesAsync();
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