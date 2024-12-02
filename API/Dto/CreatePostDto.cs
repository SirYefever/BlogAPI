using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Core.Models;

namespace API.Dto;

public class CreatePostDto
{
    [Required]
    [MaxLength(1000)]
    [MinLength(5)]
    public string Title { get; set; }
    [Required]
    [MaxLength(5000)]
    [MinLength(5)]
    public string Description { get; set; }
    
    [Required]
    public int ReadingTime { get; set; }
    [MaxLength(1000)]
    public Uri? Image { get; set; }
    public Guid? AddressId { get; set; }
    [Required]
    public List<Guid> Tags { get; set; }
}
