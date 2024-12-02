using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class CommunityPost
{
    public CommunityPost(Guid communityId, Guid postId)
    {
        CommunityId = communityId;
        PostId = postId;
    }
    [ForeignKey("Communtiy")]
    public Guid CommunityId { get; set; }
    [ForeignKey("Post")]
    public Guid PostId { get; set; }
}