using API.Dto;
using Core.InterfaceContracts;
using Core.Models;

namespace API.Converters;

public class AuthorConverters
{
    private readonly IPostLikeRepository _postLikeRepository;

    public AuthorConverters(IPostLikeRepository postLikeRepository)
    {
        _postLikeRepository = postLikeRepository;
    }

    public async Task<AuthorDto> AuthorToAuthorDto(Author author)
    {
        var dto = new AuthorDto();
        dto.FullName = author.FullName;
        dto.Posts = author.Posts;
        dto.Gender = author.Gender;
        dto.Created = author.CreateTime;
        dto.Likes = await _postLikeRepository.GetLikeCountByUserIdAsync(author.Id);
        dto.BirthDate = author.BirthDate;
        return dto;
    }
}