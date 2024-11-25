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

    public async Task<Tag> GetTagById(Guid id)
    {
        var tag = await _tagRepository.GetById(id);
        return tag;
    }

    public Task<Tag> CreateTag(string name)
    {
        throw new NotImplementedException();
    }

    public async Task<Tag> ProcessTag(string name)
    {
        var tag = await _tagRepository.GetByName(name);
        Tag newTag;
        if (tag == null)
        {
            //TODO: figure out weather it's needed to create new Tag instance here
            newTag = new Tag();
            newTag.Name = name;
            newTag.CreateTime = DateTime.Now;
            await _tagRepository.Add(newTag);
            return newTag;
        }
        return tag;
    }
}