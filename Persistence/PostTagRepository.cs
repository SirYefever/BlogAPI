using Core.InterfaceContracts;
using Core.Models;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class PostTagRepository: IPostTagRepository
{
    private readonly MainDbContext _context;

    public PostTagRepository(MainDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(PostTag postTag)
    {
        _context.Add(postTag);
        await _context.SaveChangesAsync();
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
    
    public async Task ConfirmTagExists(Guid id)
    {
        if (!await _context.Tags.AnyAsync(x => x.Id == id))
            throw new KeyNotFoundException("Tag id=" + id + " not found in database.");
    }

    public async Task AddListOfPostTags(List<Guid> tagGuids, Guid postId)
    {
        if (tagGuids.Count == 0)
            throw new BadRequestException("Specify at least one tag for a new post");
        
        foreach (var guid in tagGuids)
        {
            await ConfirmTagExists(guid);
            await CreateAsync(new PostTag(postId, guid));
        }   
    }
}