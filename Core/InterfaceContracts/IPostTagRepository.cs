using Core.Models;

namespace Core.InterfaceContracts;

public interface IPostTagRepository
{
    Task CreateAsync(PostTag postTag);
    Task<List<PostTag>> GetByPostId(Guid postId);
    Task<PostTag> GetTagsOfPost(Guid postId);
    Task<PostTag> GetPostsByTagId(Guid tagId);
    Task ConfirmTagExists(Guid id);
    Task AddListOfPostTags(List<Guid> tagGuids, Guid postId);
}