using Core.InterfaceContracts;
using Core.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<List<PostTag>> GetByPostId(Guid postId)
    {
        var postTags = _context.PostTag.AsQueryable();
        postTags = postTags.Where(x => x.PostId == postId);
        return await postTags.ToListAsync();
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