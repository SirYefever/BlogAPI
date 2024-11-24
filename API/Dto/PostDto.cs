using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace API.Dto;

public class PostDto
{
    
    [Required]
    public Guid Id { get; set; }
    [Required]
    public DateTime CreateTime { get; set; }
    [Required]//TODO: figure out weather we need [MinLength(1)] here or not
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public int ReadingTime { get; set; }
    public string Image { get; set; }
    [Required]
    public Guid AuthorId { get; set; }
    [Required]
    public string Author { get; set; }
    public Guid CommunityId { get; set; }
    public string CommunityName { get; set; }
    public Guid AdressId { get; set; }
    [Required]
    [DefaultValue(0)]
    public int Likes { get; set; }
    [Required]
    public bool HasLike { get; set; }
    [Required]
    public int CommentsCount { get; set; }
    public TagDto[] Tags { get; set; }
}