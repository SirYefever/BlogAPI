using Core.Models;

namespace Core.ServiceContracts;

public interface ITagService
{
    Task<Tag> GetTagByName(string name);
    Task<Tag> GetTagById(Guid id);
    Task<Tag> CreateTag(Tag tag);
    Task<List<Tag>> GetAllTags();
    Task<Tag> ProcessTag(string name);
}