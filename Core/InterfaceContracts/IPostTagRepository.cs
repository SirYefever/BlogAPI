using Core.Models;

namespace Core.InterfaceContracts;

public interface IPostTagRepository
{
    Task<PostTag> CreateAsync(PostTag postTag);
    Task<List<PostTag>> GetByPostId(Guid postId);
    Task<PostTag> GetTagsOfPost(Guid postId);
    Task<PostTag> GetPostsByTagId(Guid tagId);
}