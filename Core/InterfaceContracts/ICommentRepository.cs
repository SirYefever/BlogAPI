using Core.Models;

namespace Core.InterfaceContracts;

public interface ICommentRepository
{
    Task<Comment> AddAsync(Guid postId, Comment comment);
    Task<Comment> GetByIdAsync(Guid id);
    public Task<List<Comment>> GetByIdsAsync(List<Guid> ids);
    public Task IncrementSubCommentsCount(Guid commentId);
}