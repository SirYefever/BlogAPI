using System.Linq.Expressions;
using API.Dto;
using Core.InterfaceContracts;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Persistence;

public class CommunityRepository: ICommunityRepository
{
    private readonly MainDbContext _context;
    public CommunityRepository(MainDbContext context, IPostTagRepository postTagRepository)
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

    public async Task<List<Post>> GetPostsOfCommunity(CommunityPostListRequest request)
    {
        var posts = _context.Posts.AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(request.CommunityId.ToString()))
        {
            posts = posts.Where(p => p.Author.Contains(request.CommunityId.ToString()));
        }

        if (!request.TagGuids.IsNullOrEmpty())
        {
            var postTags = _context.PostTag.AsQueryable();
            var postTagsFiltered = postTags.Where(pt => request.TagGuids.Contains(pt.TagId));
            var availablePostIds = postTagsFiltered.Select(pt => pt.PostId).ToList();
            posts = posts.Where(p => availablePostIds.Contains(p.Id));
        }
        
        if (request.Sorting.HasValue)
        {
            Expression<Func<Post, object>> keySelector = request.Sorting.ToString() switch
            {
                "createAsc" => post => post.CreateTime,
                "createDesc" => post => post.CreateTime,
                "LikeAsc" => post => post.Likes,
                "LikeDesc" => post => post.Likes,
                _ => post => post.Id
            };
            if (request.Sorting.ToString().ToLower().Contains("desc"))
                posts = posts.OrderByDescending(keySelector);
            else
                posts = posts.OrderBy(keySelector);
            
            if (request.PageSize.HasValue)
            {
                posts = posts.Take(request.PageSize.Value);
            }

            if (request.Page.HasValue)
            {
                posts = posts.Skip((int)(request.PageSize.Value * (request.Page.Value - 1)));
            }
        }
        return await posts.ToListAsync();
    }
}