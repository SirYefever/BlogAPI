using API.Controllers;
using API.Dto;
using Core.Models;

namespace API.Converters;

public class PostConverters
{
    private readonly TagConverters _tagConverters;

    public PostConverters(TagConverters tagConverters)
    {
        _tagConverters = tagConverters;
    }

    public static Post PostDtoToPost(PostDto dto)
    {
        var post = new Post();
        post.Id = dto.Id;
        post.CreateTime = dto.CreateTime;
        post.Title = dto.Title;
        post.Description = dto.Description;
        post.ReadingTime = dto.ReadingTime;
        post.Author = dto.Author;
        post.AuthorId = dto.AuthorId;
        post.CommunityId = dto.CommunityId;
        post.CommunityName = dto.CommunityName;
        post.AdressId = dto.AdressId;
        post.Image  = dto.Image;
        post.Likes = dto.Likes;
        post.AuthorId = dto.AuthorId;
        post.Author = dto.Author;
        post.CommentsCount = dto.CommentsCount;
        return post;
    }
    
    public static PostDto PostToPostDto(Post post)
    {
        var dto = new PostDto();
        dto.Id = post.Id;
        dto.CreateTime = post.CreateTime;
        dto.Title = post.Title;
        dto.Description = post.Description;
        dto.ReadingTime = post.ReadingTime;
        dto.Author = post.Author;
        dto.AuthorId = post.AuthorId;
        dto.CommunityId = post.CommunityId;
        dto.CommunityName = post.CommunityName;
        dto.AdressId = post.AdressId;
        dto.Image  = post.Image;
        dto.Likes = post.Likes;
        dto.AuthorId = post.AuthorId;
        dto.Author = post.Author;
        dto.CommentsCount = post.CommentsCount;
        return dto;
    }
    
    public async Task<Post> CreatePostDtoToPost(CreatePostDto dto)
    {
        var post = new Post();
        post.Title = dto.Title;
        post.Description = dto.Description;
        post.ReadingTime = dto.ReadingTime;
        post.Image  = dto.Image;
        post.AdressId = dto.AddressId;
        return post;
    }
}