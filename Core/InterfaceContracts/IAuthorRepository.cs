using Core.Models;

namespace Core.InterfaceContracts;

public interface IAuthorRepository
{
    Task<List<Author>> GetAllAsync();
}