using System.Runtime.InteropServices.ComTypes;
using Core.InterfaceContracts;
using Core.Models;
using Infrastructure.Exceptions;
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

        if (comment.ParentId != null && !await _context.Comment.AnyAsync(x => x.Id == comment.ParentId))
            throw new KeyNotFoundException("Comment with id=" + comment.ParentId.ToString() + " not found in database");

        if (!await _context.Posts.AnyAsync(x => x.Id == postId))
            throw new KeyNotFoundException("Post with id=" + postId.ToString() + " not found in database");

        var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == postId);
        post.CommentsCount++;
        if (post == null)
            throw new KeyNotFoundException("Post with id=" + postId.ToString() + " not found in database");

        var community = await _context.Communities.FirstOrDefaultAsync(x => x.Id == post.CommunityId);
        if (community != null)
            if (!await _context.UserCommunity.AnyAsync(x =>
                    x.CommunityId == post.CommunityId && x.UserId == comment.AuthorId && community.IsClosed))
                throw new ForbiddenException("Access to closed community post with id=" + post.Id + " is forbidden for user id=" + comment.AuthorId);

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
        if (!await _context.Comment.AnyAsync(c => c.Id == commentId))
            throw new KeyNotFoundException("Comment with id=" + commentId.ToString() + "  not found in  database");
        var replies = await _context.Comment.Where(c => c.ParentId == commentId).ToListAsync();
        return replies;
    }

    public async Task UpdateAsync(Guid commentId, string newContent, Guid userId)
    {
        var comment = await _context.Comment.FirstOrDefaultAsync(c => c.Id == commentId);
        if (comment == null)
            throw new KeyNotFoundException("Comment with id=" + commentId.ToString() + " not found in database");

        if (comment.AuthorId != userId)
            throw new ForbiddenException("The user with id=" + userId + " is not the author of the comment");
        
        comment.Content = newContent;
        comment.ModifiedDate = DateTime.UtcNow;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid commentId, Guid userId)
    {
        var comment = await _context.Comment.FirstOrDefaultAsync(c => c.Id == commentId); 
        
        if (comment == null)
            throw new KeyNotFoundException("Comment with id=" + commentId.ToString() + " not found in database");
        
        if (comment.AuthorId != userId)
            throw new ForbiddenException("The user with id=" + userId + " is not the author of the comment");
        
        var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == comment.postId);
        post.CommentsCount--;
        comment.Content = "";
        comment.ModifiedDate = DateTime.UtcNow;
        comment.DeleteDate = DateTime.UtcNow;
        await _context.SaveChangesAsync();
    }

    public async Task<List<Comment>> GetCommentsByPostId(Guid postId)
    {
        var comments = _context.Comment.AsQueryable();
        comments = comments.Where(c => c.postId == postId && c.ParentId == null);
        return await comments.ToListAsync();
    }
}