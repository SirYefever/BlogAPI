using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class PostLike
{
    public PostLike() { }
    public PostLike(Guid postId, Guid userId)
    {
        PostId = postId;
        UserWhoLikedId = userId;
    }
    [ForeignKey("Post")]
    public Guid PostId { get; set; }
    [ForeignKey("UserWhoLiked")]
    public Guid UserWhoLikedId { get; set; }
}