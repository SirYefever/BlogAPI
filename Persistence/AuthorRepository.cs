using Core.InterfaceContracts;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class AuthorRepository: IAuthorRepository
{
    private readonly MainDbContext _context;

    public AuthorRepository(MainDbContext context)
    {
        _context = context;
    }

    public async Task<List<Author>> GetAllAsync()
    {
        var authors = new List<Author>();
        var posts = await _context.Posts.ToListAsync();
        foreach (var post in posts)
        {
            var authorOfPost = authors.FirstOrDefault(x => x.Id == post.AuthorId);
            if (authorOfPost == null)
            {
                var authorAsUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == post.AuthorId);
                
                if (authorAsUser == null)
                    continue;
                
                var newAuthor = new Author(post, authorAsUser);
                authors.Add(newAuthor);
            }
            else
                authorOfPost.Posts++;
        }
        return authors;
    }
}