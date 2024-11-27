using System.ComponentModel.DataAnnotations;

namespace API.Dto;

public class CreateCommunityDto
{
     [Required]
     public string Name { get; set; }
     public string? Description { get; set; }
     public bool IsClosed { get; set; }
}