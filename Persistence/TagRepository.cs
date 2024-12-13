using Core.InterfaceContracts;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class TagRepository : ITagRepository
{
    private readonly MainDbContext _context;

    public TagRepository(MainDbContext context)
    {
        _context = context;
    }

    public async Task Add(Tag tag)
    {
        if (await _context.Tags.AnyAsync(x => x.Name == tag.Name))
            return;

        _context.Tags.Add(tag);
        await _context.SaveChangesAsync();
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

    public async Task ConfirmTagExists(Guid id)
    {
        if (!await _context.Tags.AnyAsync(x => x.Id == id))
            throw new KeyNotFoundException("Tag id=" + id + " not found in database.");
    }
}