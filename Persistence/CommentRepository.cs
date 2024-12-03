using System.Runtime.InteropServices.ComTypes;
using Core.InterfaceContracts;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class CommentRepository: ICommentRepository
{

    private readonly MainDbContext _context;

    public CommentRepository(MainDbContext context)
    {
        _context = context;
    }

    public async Task<Comment> AddAsync(Guid postId, Comment comment)
    {
        comment.postId = postId;
        await _context.Comment.AddAsync(comment);
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment> GetByIdAsync(Guid id)
    {
        var comment = await _context.Comment.FirstAsync(c => c.Id == id);
        return comment;
    }

    public async Task<List<Comment>> GetByIdsAsync(List<Guid> ids)
    {
        var comments = await _context.Comment.Where(c => ids.Contains(c.Id)).ToListAsync();
        return comments;
    }

    public async Task IncrementSubCommentsCount(Guid commentId)
    {
        var comment = await _context.Comment.FirstAsync(c => c.Id == commentId);
        comment.SubComments++;
        await _context.SaveChangesAsync();
    }

    public async Task<List<Comment>> GetRepliesByIdAsync(Guid commentId)
    {
        var replies = await _context.Comment.Where(c => c.ParentId == commentId).ToListAsync();
        return replies;
    }

    public async Task UpdateAsync(Guid commentId, string newContent)
    {
        var comment = _context.Comment.First(c => c.Id == commentId);
        comment.Content = newContent;
        comment.ModifiedDate = DateTime.UtcNow;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid commentId)
    {
        var comment = await _context.Comment.FirstAsync(c => c.Id == commentId); 
        comment.Content = "";
        comment.ModifiedDate = DateTime.UtcNow;
        comment.DeleteDate = DateTime.UtcNow;
        await _context.SaveChangesAsync();
    }

    public async Task<List<Comment>> GetCommentsByPostId(Guid postId)
    {
        var comments = _context.Comment.AsQueryable();
        comments = comments.Where(c => c.postId == postId);
        return await comments.ToListAsync();
    }
}