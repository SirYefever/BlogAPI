namespace Core.Models;

public class Comment
{
    public Comment()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    public Guid postId { get; set; }
    public DateTime CreateTime { get; set; }
    public string Content { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public DateTime? DeleteDate { get; set; }
    public Guid AuthorId { get; set; }
    public string Author { get; set; }
    public int SubComments { get; set; } = 0;
    public Guid? ParentId { get; set; }
}