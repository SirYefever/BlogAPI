using Core.InterfaceContracts;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Persistence;

public class CommunityRepository: ICommunityRepository
{
    private readonly MainDbContext _context; 
    public CommunityRepository(MainDbContext context)
    {
        _context = context;
    }
    public async Task<Community> Add(Community community)
    {
        _context.Communities.Add(community);
        await _context.SaveChangesAsync();
        return community;
    }

    public async Task<Community> GetById(Guid id)
    {
        var community = await _context.Communities.SingleOrDefaultAsync(c => c.Id == id);
        if (community == null)
        {
            //TODO: figure out how to handle this case
            throw new ArgumentException("Community not found");
        }
        return community;
    }

    public async Task<List<Community>> GetAll()
    {
        return await _context.Communities.ToListAsync();
    }

    public Task<Community> UpdateById(Guid id, Community newCommunity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteById(Guid id)
    {
        throw new NotImplementedException();
    }
}