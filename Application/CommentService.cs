using Core.InterfaceContracts;
using Core.Models;
using Core.ServiceContracts;

namespace Application;

public class CommentService: ICommentService
{
    private readonly ICommentRepository _commentRepository;

    public CommentService(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<Comment> CreateCommentAsync(Guid postId, Comment comment)
    {
        await _commentRepository.AddAsync(postId, comment);
        if (comment.ParentId != null)
        {
            await _commentRepository.IncrementSubCommentsCount((Guid)comment.ParentId);
        }
        return comment;
    }

    public async Task UpdateCommentAsync(Guid oldCommentId, string newContent)
    {
        await _commentRepository.UpdateAsync(oldCommentId, newContent);
    }

    public Task DeleteCommentAsync(Guid commentId)
    {
        throw new NotImplementedException();
    }

    public Task<Comment> GetCommentAsync(Guid commentId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Comment>> GetReplies(Guid commentId)
    {
        var replies = await _commentRepository.GetRepliesByIdAsync(commentId);
        return replies;
    }
}