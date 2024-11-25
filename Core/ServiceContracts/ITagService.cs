using Core.Models;

namespace Core.ServiceContracts;

public interface ITagService
{
    Task<Tag> GetTagByName(string name);
    Task<Tag> GetTagById(Guid id);
    Task<Tag> CreateTag(string name);
    Task<Tag> ProcessTag(string name);
}