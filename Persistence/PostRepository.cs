using System.Linq.Expressions;
using API.Dto;
using Azure.Core;
using Core.InterfaceContracts;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class PostRepository: IPostRepository
{
    private readonly MainDbContext _context; 
    public PostRepository(MainDbContext context)
    {
        _context = context;
    }
    public async Task<Post> Add(Post post)
    {//TODO: figure out weather this needs to be wrapped with try catch
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
        return post;
    }

    public Task<Post> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public List<Post> GetAvailabePosts(PostListRequest request)
    {//TODO: figure out why nothing is awaitable here
        var posts = _context.Posts.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.PartOfAuthorName))
        {
            posts = posts.Where(p => p.Author.Contains(request.PartOfAuthorName));
        }

        if (request.MinReadingTime.HasValue)
        {
            posts = posts.Where(p => p.ReadingTime > request.MinReadingTime.Value);
        }

        if (request.MaxReadingTime.HasValue)
        {
            posts = posts.Where(p => p.ReadingTime < request.MaxReadingTime.Value);
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
            {
                posts = posts.OrderByDescending(keySelector);
            }
            else
            {
                posts = posts.OrderBy(keySelector);
            }
        }

        // if (request.OnlyMyCommunities)//TODO: finish this after implementing communities.
        // {
        //     posts = posts.Where(p => p.OnlyMyCommunities);
        // }

        if (request.PageSize.HasValue)
        {
            posts = posts.Take(request.PageSize.Value);
        }

        if (request.Page.HasValue)
        {
            posts = posts.Skip((int)(request.PageSize.Value * (request.Page.Value - 1)));
        }

        return posts.ToList();
    }

    public Task AddLike(Guid postId, Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveLike(Guid postId, Guid userId)
    {
        throw new NotImplementedException();
    }
}