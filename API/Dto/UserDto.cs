using Core.Models;
namespace API.Dto;

public class UserDto
{
    //TODO: Dedicate weather we need database-attributes here
    public Guid Id { get; set; }
    public DateTime? CreateTime { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public DateTime? BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string? PhoneNumber { get; set; }
}