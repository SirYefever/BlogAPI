using Core.Models;

namespace Core.ServiceContracts;

public interface ITagService
{
    Task<Tag> GetTagById(Guid id);
    Task<List<Tag>> GetAllTags();
}