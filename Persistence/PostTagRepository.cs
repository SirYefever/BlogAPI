using Core.InterfaceContracts;
using Core.Models;

namespace Persistence;

public class PostTagRepository: IPostTagRepository
{
    private readonly MainDbContext _context;

    public PostTagRepository(MainDbContext context)
    {
        _context = context;
    }

    public async Task<PostTag> CreateAsync(PostTag postTag)
    {
        _context.Add(postTag);
        await _context.SaveChangesAsync();
        return postTag;
    }

    public Task<PostTag> GetAllAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<PostTag> GetTagsOfPost(Guid postId)
    {
        throw new NotImplementedException();
    }

    public Task<PostTag> GetPostsByTagId(Guid tagId)
    {
        throw new NotImplementedException();
    }
}