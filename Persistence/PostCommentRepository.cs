using Core.InterfaceContracts;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class PostCommentRepository: IPostCommentRepository
{
    private readonly MainDbContext _context;

    public PostCommentRepository(MainDbContext context)
    {
        _context = context;
    }

    public async Task<PostComment> Add(PostComment postComment)
    {
        _context.PostComment.Add(postComment);
        await _context.SaveChangesAsync();
        return postComment;
    }

    public async Task<List<PostComment>> GetByPostId(Guid postId)
    {
        var postComments = _context.PostComment.AsQueryable();
        postComments = postComments.Where(pc => pc.PostId == postId);
        return await postComments.ToListAsync();
    }
}