using Core.Models;

namespace Core.InterfaceContracts;

public interface ITagRepository
{
    Task Add(Tag tag);
    Task<Tag> GetById(Guid id);
    Task<List<Tag>> GetByIdsAsync(List<Guid> id);
    Task<Tag> GetByName(string name);
    Task<List<Tag>> GetAll();
    Task ConfirmTagExists(Guid id);
}