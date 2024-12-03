using API.Controllers;
using API.Dto;
using Core.InterfaceContracts;
using Core.Models;

namespace API.Converters;

public class PostConverters
{
    private readonly TagConverters _tagConverters;
    private readonly IPostCommentRepository _postCommentRepository;
    private readonly IPostTagRepository _postTagRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly ITagRepository _tagRepository;

    public PostConverters(TagConverters tagConverters, IPostCommentRepository postCommentRepository, IPostTagRepository postTagRepository, ICommentRepository commentRepository, ITagRepository tagRepository)
    {
        _tagConverters = tagConverters;
        _postCommentRepository = postCommentRepository;
        _postTagRepository = postTagRepository;
        _commentRepository = commentRepository;
        _tagRepository = tagRepository;
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
    
    public static Post CreatePostDtoToPost(CreatePostDto dto, Guid communityId)
    {
        var post = new Post();
        post.Title = dto.Title;
        post.Description = dto.Description;
        post.ReadingTime = dto.ReadingTime;
        post.Image  = dto.Image;
        post.AdressId = dto.AddressId;
        post.CommunityId = communityId;
        return post;
    }

    public async Task<PostFullDto> PostToPostFullDto(Post post)
    {
        var dto = new PostFullDto();
        dto.Id = post.Id;
        dto.Title = post.Title;
        dto.Description = post.Description;
        dto.CreateTime = post.CreateTime;
        dto.ReadingTime = post.ReadingTime;
        dto.Image = post.Image;
        dto.Author = post.Author;
        dto.AuthorId = post.AuthorId;
        dto.CommunityId = post.CommunityId;
        dto.CommunityName = post.CommunityName;
        dto.AdressId = post.AdressId;
        dto.CommentsCount = post.CommentsCount;
        
        var postComments = await _postCommentRepository.GetByPostId(post.Id);
        var commentIds = postComments.Select(pc => pc.CommentId).ToList();
        var comments = await _commentRepository.GetByIdsAsync(commentIds);
        dto.Comments = comments;
        
        var postTags = await _postTagRepository.GetByPostId(post.Id);
        var tagIds = postTags.Select(pt => pt.TagId).ToList();
        var tags = await _tagRepository.GetByIdsAsync(tagIds);
        var tagDtos = tags.Select(tag => TagConverters.TagToTagDto(tag)).ToList();
        dto.Tags = tagDtos;
        return dto;
    }
}