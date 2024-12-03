using Core.Models;

namespace Core.InterfaceContracts;

public interface IPostCommentRepository
{
    Task<PostComment> Add(PostComment postComment);
    Task<List<PostComment>> GetByPostId(Guid postId);
}