using System.Linq.Expressions;
using API.Dto;
using Core.InterfaceContracts;
using Core.Models;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
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
        var community = await _context.Communities.FirstOrDefaultAsync(c => c.Id == id);
        
        if (community == null)
            throw new KeyNotFoundException("Community id=" + id.ToString() + " not found in database.");
        
        return community;
    }

    public async Task<List<Community>> GetAll()
    {
        return await _context.Communities.ToListAsync();
    }

    public async Task<List<Post>> GetPostsOfCommunity(CommunityPostListRequest request, List<PostLike> likes, Guid userId)
    {
        var community = await _context.Communities.FirstOrDefaultAsync(x => x.Id == request.CommunityId);
        
        if (community.IsClosed && await _context.UserCommunity.AnyAsync(x => x.CommunityId == community.Id && x.UserId == userId))
            throw new ForbiddenException("User id=" + userId.ToString() + "does not belong to closed community id=" + community.Id.ToString());
        
        if (community == null)
            throw new KeyNotFoundException("Community with id=" + request.CommunityId.ToString() + " not found in database");
        
        var posts = _context.Posts.AsQueryable();
        
        posts = posts.Where(p => p.CommunityId == request.CommunityId);

        if (request.TagGuids != null && request.TagGuids.Any()) {
            var postTags = _context.PostTag.AsQueryable();
            var postTagsFiltered = postTags.Where(pt => request.TagGuids.Contains(pt.TagId));
            var availablePostIds = await postTagsFiltered.Select(pt => pt.PostId).ToListAsync();
            posts = posts.Where(p => availablePostIds.Contains(p.Id));
        }
        
        if (request.Sorting.HasValue)
        {
            Expression<Func<Post, object>> keySelector = request.Sorting.ToString() switch
            {
                "createAsc" => post => post.CreateTime,
                "createDesc" => post => post.CreateTime,
                "LikeAsc" => post => likes.Select(pl => pl.PostId == post.Id).Count(),
                "LikeDesc" => post =>  likes.Select(pl => pl.PostId == post.Id).Count(),
                _ => post => post.Id
            };
            if (request.Sorting.ToString().ToLower().Contains("desc"))
                posts = posts.OrderByDescending(keySelector);
            else
                posts = posts.OrderBy(keySelector);
        }
        
        if (request.PageSize.HasValue)
            posts = posts.Take(request.PageSize.Value);

        if (request.Page.HasValue)
            posts = posts.Skip((int)(request.PageSize.Value * (request.Page.Value - 1)));
        
        return await posts.ToListAsync();
    }

    public async Task<int> GetPostQuantity(Guid communityId)
    {
        if (! await _context.Communities.AnyAsync(c => c.Id == communityId))
            throw new KeyNotFoundException("Community with id=" + communityId.ToString() + " not found in database");
        
        return await _context.Posts.CountAsync(p => p.CommunityId == communityId);
    }
}