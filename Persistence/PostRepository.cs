using API.Dto;
using Core.InterfaceContracts;
using Core.Models;

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

    public List<Post> GetAvailabePosts()//TODO: figure out if it's OK that there are no 'await's here.
    {
        List<Post> result = new List<Post>();
        foreach (var post in _context.Posts)
        {
            result.Add(post);
        }

        return result;
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