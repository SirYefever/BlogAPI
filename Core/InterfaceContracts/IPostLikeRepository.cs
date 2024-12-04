using Core.Models;

namespace Core.InterfaceContracts;

public interface IPostLikeRepository
{
    public Task AddAsync(PostLike postLike);
    public Task DeleteAsync(Guid postId, Guid userId);
}