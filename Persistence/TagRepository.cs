using Core.InterfaceContracts;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class TagRepository: ITagRepository
{
    private readonly MainDbContext _context; 
    public TagRepository(MainDbContext context)
    {
        _context = context;
    }
    
    public async Task<Tag> Add(Tag tag)
    {
        _context.Tags.FirstOrDefaultAsync(c => c.Name == tag.Name);
        if (tag != null)
            throw new ArgumentException($"Tag {tag.Name} already exists");
        _context.Tags.Add(tag);
        await _context.SaveChangesAsync();
        return tag;
    }

    public async Task<Tag> GetById(Guid id)
    {
        var tag = await _context.Tags.FirstOrDefaultAsync(x => x.Id == id);
        return tag;
    }

    public async Task<List<Tag>> GetByIdsAsync(List<Guid> id)
    {
        return await _context.Tags.Where(x => id.Contains(x.Id)).ToListAsync();
    }

    public async Task<Tag> GetByName(string name)
    {
        var tag = await _context.Tags.FirstOrDefaultAsync(x => x.Name == name);
        return tag;
    }

    public async Task<List<Tag>> GetAll()
    {
        return await _context.Tags.ToListAsync();
    }
}