using System.ComponentModel.DataAnnotations;
using Core.Models;

namespace API.Dto;

public class AuthorDto
{
    [Required]
    public string FullName { get; set; }
    public DateTime? BirthDate { get; set; }
    public Gender Gender { get; set; }
    public int? Posts { get; set; }
    public int? Likes { get; set; }
    public DateTime? CreateTime { get; set; }
}