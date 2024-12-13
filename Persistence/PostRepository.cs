using System.Linq.Expressions;
using Core.InterfaceContracts;
using Core.Models;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class PostRepository : IPostRepository
{
    private readonly MainDbContext _context;

    public PostRepository(MainDbContext context)
    {
        _context = context;
    }

    public async Task<Post> Add(Post post)
    {
        if (!await _context.UserCommunity.AnyAsync(x => x.CommunityId == post.CommunityId && x.UserId == post.AuthorId))
            throw new ForbiddenException(
                "User id=" + post.AuthorId + " is not able to post in community id=" + post.CommunityId);

        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
        return post;
    }

    public async Task AddPersonal(Post post)
    {
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
    }

    public async Task<Post> GetById(Guid id, Guid userId)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(post => post.Id == id);
        if (post == null)
            throw new KeyNotFoundException("Post id=" + id + " not found in database.");

        if (post.CommunityId != null)
        {
            var community = await _context.Communities.FirstOrDefaultAsync(x => x.Id == post.CommunityId);
            if (community == null)
                throw new KeyNotFoundException("Community id=" + post.CommunityId +
                                               " does not exist in database anymore.");
            if (community.IsClosed &&
                !await _context.UserCommunity.AnyAsync(x => x.CommunityId == community.Id && x.UserId == userId))
                throw new ForbiddenException("User id=" + userId + "does not belong to closed community id=" +
                                             community.Id);
        }

        return post;
    }

    public async Task<List<Post>> GetAvailabePosts(PostListRequest request, Guid userId, List<PostLike> postLikes,
        List<UserCommunity> curUserCommunities = null)
    {
        var posts = _context.Posts.AsQueryable();


        if (request.OnlyMyCommunities)
        {
            var availableCommunities = curUserCommunities.Select(uc => uc.CommunityId).ToList();
            posts = posts.Where(p => p.CommunityId != null && availableCommunities.Contains((Guid)p.CommunityId));
        }

        if (!string.IsNullOrWhiteSpace(request.PartOfAuthorName))
            posts = posts.Where(p => p.Author.Contains(request.PartOfAuthorName));

        if (request.MinReadingTime.HasValue) posts = posts.Where(p => p.ReadingTime > request.MinReadingTime.Value);

        if (request.MaxReadingTime.HasValue) posts = posts.Where(p => p.ReadingTime < request.MaxReadingTime.Value);

        if (request.Sorting.HasValue)
        {
            Expression<Func<Post, object>> keySelector = request.Sorting.ToString() switch
            {
                "createAsc" => post => post.CreateTime,
                "createDesc" => post => post.CreateTime,
                "LikeAsc" => post => postLikes.Select(pl => pl.PostId == post.Id).Count(),
                "LikeDesc" => post => postLikes.Select(pl => pl.PostId == post.Id).Count(),
                _ => post => post.Id
            };
            if (request.Sorting.ToString().ToLower().Contains("desc"))
                posts = posts.OrderByDescending(keySelector);
            else
                posts = posts.OrderBy(keySelector);
        }

        if (request.Tags != null && request.Tags.Any())
        {
            var postTags = _context.PostTag.AsQueryable();
            var postTagsFiltered = postTags.Where(pt => request.Tags.Contains(pt.TagId));
            var availablePostIds = postTagsFiltered.Select(pt => pt.PostId);
            posts = posts.Where(p => availablePostIds.Contains(p.Id));
        }

        if (request.PageSize.HasValue) posts = posts.Take(request.PageSize.Value);

        if (request.Page.HasValue) posts = posts.Skip(request.PageSize.Value * (request.Page.Value - 1));

        return await posts.ToListAsync();
    }
}