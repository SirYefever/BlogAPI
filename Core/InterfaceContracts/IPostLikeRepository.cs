using Core.Models;

namespace Core.InterfaceContracts;

public interface IPostLikeRepository
{
    public Task<List<PostLike>> GetAllAsync();
    public Task AddAsync(PostLike postLike);
    public Task DeleteAsync(Guid postId, Guid userId);
    public Task<int> GetLikeCountByPostIdAsync(Guid postId);
    public Task<int> GetLikeCountByUserIdAsync(Guid userId);
}