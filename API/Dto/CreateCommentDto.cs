using System.ComponentModel.DataAnnotations;

namespace API.Dto;

public class CreateCommentDto
{
    [Required]
    [MaxLength(1000)]
    public string Content { get; set; }
    public Guid? ParentId { get; set; }
}