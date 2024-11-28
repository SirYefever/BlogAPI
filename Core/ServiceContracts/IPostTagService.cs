using Core.Models;

namespace Core.ServiceContracts;

public interface IPostTagService
{
    public Task<PostTag> AddTagToPost(Post post, Tag tag);
    public Task<PostTag> RemoveTagFromPost(Post post, Tag tag);
}