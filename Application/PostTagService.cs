using Core.InterfaceContracts;
using Core.Models;
using Core.ServiceContracts;

namespace Application;

public class PostTagService : IPostTagService
{
    private readonly IPostTagRepository _postTagRepository;

    public PostTagService(IPostTagRepository postTagRepository)
    {
        _postTagRepository = postTagRepository;
    }

    public async Task<PostTag> AddTagToPost(Post post, Tag tag)
    {
        var postTag = new PostTag(post.Id, tag.Id);
        await _postTagRepository.CreateAsync(postTag);
        return postTag;
    }

    public Task<PostTag> RemoveTagFromPost(Post post, Tag tag)
    {
        throw new NotImplementedException();
    }
}