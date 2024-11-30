using Core.Models;

namespace Core.ServiceContracts;

public interface IAuthorService
{
    public Task<List<Author>> GetAllAsync();
}