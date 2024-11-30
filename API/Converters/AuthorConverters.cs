using API.Dto;
using Core.Models;

namespace API.Converters;

public class AuthorConverters
{
    public static AuthorDto AuthorToAuthorDto(Author author)
    {
        var dto = new AuthorDto();
        dto.FullName = author.FullName;
        dto.Posts = author.Posts;
        dto.Gender = author.Gender;
        dto.Created = author.CreateTime;
        dto.Likes = author.Likes;
        dto.BirthDate = author.BirthDate;
        return dto;
    }
}