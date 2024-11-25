using API.Dto;
using Core.Models;

namespace API.Controllers;

public class TagConverters
{
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
        tag.Name = dto.Name;
        tag.CreateTime = dto.CreateTime;
        return tag;
    }
    
    public static List<Tag> TagDtoListToTagList(List<TagDto> dtos)
    {
        var tagList = new List<Tag>();
        foreach (var el in dtos)
        {
            tagList.Add(TagDtoToTag(el));
        }
        return tagList;
    }
}