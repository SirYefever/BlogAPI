using Core.Models;

namespace API.Dto;

public class CommunityUserDto
{
    public Guid UserId { get; set; }
    public Guid CommunityId { get; set; }
    public CommunityRole Role { get; set; }
    
}