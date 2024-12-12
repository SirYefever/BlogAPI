using System.ComponentModel.DataAnnotations;

namespace API.Dto;

public class CreateCommunityDto
{
     [MaxLength(50)]
     [Required]
     public string Name { get; set; }
     [MaxLength(10000)]
     public string? Description { get; set; }
     [Required]
     public bool IsClosed { get; set; }
}