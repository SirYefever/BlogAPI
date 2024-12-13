using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class PostLike
{
    public PostLike()
    {
    }

    public PostLike(Guid postId, Guid userId, Guid postAuthorId)
    {
        PostId = postId;
        UserWhoLikedId = userId;
        PostAuthorId = postAuthorId;
    }

    [ForeignKey("Post")] public Guid PostId { get; set; }

    [ForeignKey("UserWhoLiked")] public Guid UserWhoLikedId { get; set; }

    [ForeignKey("PostAuthorId")] public Guid PostAuthorId { get; set; }
}