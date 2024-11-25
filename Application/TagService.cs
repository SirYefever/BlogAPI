using Core.InterfaceContracts;
using Core.Models;
using Core.ServiceContracts;

namespace Application;

public class TagService: ITagService
{
    private readonly ITagRepository _tagRepository;

    public TagService(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public Task<Tag> GetTagByName(string name)
    {
        throw new NotImplementedException();
    }

    public Task<Tag> GetTagById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Tag> CreateTag(string name)
    {
        throw new NotImplementedException();
    }

    public await Task<Tag> ProcessTag(string name)
    {
        var tag = _tagRepository.GetByName(name);
        if (tag == null)
        {
            //create new tag
            _tagRepository.Add();
        }
}