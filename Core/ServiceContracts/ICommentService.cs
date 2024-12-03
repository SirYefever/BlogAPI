using Core.Models;

namespace Core.ServiceContracts;

public interface ICommentService
{
    Task<Comment> CreateCommentAsync(Guid postId, Comment comment);
    Task<Comment> UpdateCommentAsync(Guid oldCommentId, Comment newComment);
    Task DeleteCommentAsync(Guid commentId);
    Task<Comment> GetCommentAsync(Guid commentId);
}