using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class PostTag
{
    public PostTag(Guid postId, Guid tagId)
    {
        PostId = postId;
        TagId = tagId;
    }

    [ForeignKey("Post")] public Guid PostId { get; set; }

    [ForeignKey("Tag")] public Guid TagId { get; set; }
}