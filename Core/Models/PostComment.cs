using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class PostComment
{
    public PostComment(Guid postId, Guid commentId)
    {
        PostId = postId;
        CommentId = commentId;
    }
    [ForeignKey("Post")]
    public Guid PostId { get; set; }
    [ForeignKey("Comment")]
    public Guid CommentId { get; set; }
}