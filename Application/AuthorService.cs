using Core.InterfaceContracts;
using Core.Models;
using Core.ServiceContracts;

namespace Application;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<List<Author>> GetAllAsync()
    {
        var authors = await _authorRepository.GetAllAsync();
        return authors;
    }
}