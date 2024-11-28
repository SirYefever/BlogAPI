using System.ComponentModel.DataAnnotations;

namespace API.Dto;

public class CreateTagDto
{
    [Required]
    public string Name { get; set; }
}