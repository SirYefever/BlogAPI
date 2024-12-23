namespace Core.Models;

public class Post
{
    public Post()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    public DateTime CreateTime { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int ReadingTime { get; set; }
    public Uri? Image { get; set; }
    public Guid AuthorId { get; set; }
    public string Author { get; set; }
    public Guid? CommunityId { get; set; }
    public string? CommunityName { get; set; }
    public Guid? AdressId { get; set; }
    public int CommentsCount { get; set; }
}