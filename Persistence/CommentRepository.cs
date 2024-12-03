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

    public async Task<Comment> AddAsync(Comment comment)
    {
        _context.Comment.Add(comment);
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
}