using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class UserCommunity
{
    public UserCommunity()
    {
    }

    public UserCommunity(Guid communityId, Guid userId, CommunityRole role)
    {
        UserId = userId;
        CommunityId = communityId;
        HighestRole = role;
    }

    [ForeignKey("User")] public Guid UserId { get; set; }

    [ForeignKey("Community")] public Guid CommunityId { get; set; }

    public CommunityRole HighestRole { get; set; }
}