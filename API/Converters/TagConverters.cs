using API.Dto;
using Core.Models;
using Core.ServiceContracts;

namespace API.Controllers;

public class TagConverters
{
    
    private readonly ITagService _tagService;

    public TagConverters(ITagService tagService)
    {
        _tagService = tagService;
    }

    public static TagDto TagToTagDto(Tag tag)
    {
        var tagDto = new TagDto();
        tagDto.Id = tag.Id;
        tagDto.Name = tag.Name;
        tagDto.CreateTime = tag.CreateTime;
        return tagDto;
    }

    public static List<TagDto> TagListToTagDtoList(List<Tag> tags)
    {
        var tagDtoList = new List<TagDto>();
        foreach (var el in tags)
        {
            tagDtoList.Add(TagToTagDto(el));
        }
        return tagDtoList;
    }
    
    public static Tag TagDtoToTag(TagDto dto)
    {
        var tag = new Tag();
        tag.Id = dto.Id;
        tag.CreateTime = dto.CreateTime;
        tag.Name = dto.Name;
        
        return tag;
    }

    public static List<Tag> TagDtoListToTagList(List<TagDto> dtoList)
    {
       var tagList = new List<Tag>();
       foreach (var dto in dtoList)
       {
           tagList.Add(TagDtoToTag(dto));
       }
       return tagList;
    }
    
    public async Task<List<Tag>> TagGuidListToTagList(List<Guid> dtoList)
    {
        var tagList = new List<Tag>();
        foreach (var id in dtoList)
        {
            tagList.Add(await _tagService.GetTagById(id));
        }
        return tagList;
    }
}