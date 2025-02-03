using Core.Models;

namespace Core.ServiceContracts;

public interface ICommentService
{
    Task<Comment> CreateCommentAsync(Guid postId, Comment comment);
    Task UpdateCommentAsync(Guid oldCommentId, string newContent, Guid userId);
    Task DeleteCommentAsync(Guid commentId, Guid userId);
    Task<List<Comment>> GetReplies(Guid commentId);
}