using System.ComponentModel.DataAnnotations;

namespace API.Dto;

public class UpdateCommentDto
{
    [Required]
    [MaxLength(1000)]
    public string Content { get; set; }
}