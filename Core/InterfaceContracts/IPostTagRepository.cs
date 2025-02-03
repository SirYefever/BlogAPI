using Core.Models;

namespace Core.InterfaceContracts;

public interface IPostTagRepository
{
    Task CreateAsync(PostTag postTag);
    Task<List<PostTag>> GetByPostId(Guid postId);
    Task AddListOfPostTags(List<Guid> tagGuids, Guid postId);
}