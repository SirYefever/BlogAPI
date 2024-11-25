using Core.Models;

namespace Core.InterfaceContracts;

public interface ITagRepository
{
    Task<Tag> Add(Tag tag);
    Task<Tag> GetById(Guid id);
    Task<Tag> GetByName(string name);
}