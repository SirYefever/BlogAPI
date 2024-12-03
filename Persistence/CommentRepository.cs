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
        _context.Comment.Add(comment);
        _context.PostComment.Add(new PostComment(postId, comment.Id));
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
}