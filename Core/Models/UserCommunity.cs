using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class UserCommunity
{
    public UserCommunity(Guid userId, Guid communityId)
    {
        UserId = userId;
        CommunityId = communityId;
    }
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    [ForeignKey("Community")]
    public Guid CommunityId { get; set; }
}