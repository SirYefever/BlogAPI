using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Core.Models;

namespace API.Dto;

public class PostFullDto
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public DateTime CreateTime { get; set; }
    [Required] 
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public int ReadingTime { get; set; }
    public Uri Image { get; set; }
    [Required]
    public Guid AuthorId { get; set; }
    [Required]
    public string? Author { get; set; }
    public Guid? CommunityId { get; set; }
    public string? CommunityName { get; set; }
    public Guid? AdressId { get; set; }
    [Required]
    [DefaultValue(0)]
    public int Likes { get; set; }
    [Required]
    public bool HasLike { get => Likes > 0; }
    [Required]
    public int CommentsCount { get; set; }
    public List<TagDto>? Tags { get; set; } 
    [Required]
    public List<Comment>? Comments { get; set; } 
}