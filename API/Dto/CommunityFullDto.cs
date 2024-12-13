using System.ComponentModel.DataAnnotations;

namespace API.Dto;

public class CommunityFullDto
{
    public Guid Id { get; set; }
    public DateTime CreateTime { get; set; }

    [Required] public string Name { get; set; }

    public string? Description { get; set; }
    public bool IsClosed { get; set; }
    public int SubscribersCount { get; set; } = 0; //TODO: figure out weather this sets the default value to zero.
    public List<UserDto> Administrators { get; set; }
}