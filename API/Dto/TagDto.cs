using System.ComponentModel.DataAnnotations;

namespace API.Dto;

public class TagDto
{
    [Required] public Guid Id { get; set; }

    [Required] public DateTime CreateTime { get; set; }

    [Required] public string Name { get; set; }
}