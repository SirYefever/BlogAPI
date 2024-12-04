namespace Core.Models;

public class Author
{
    public Author(Post post, User user)
    {
        Id = user.Id;
        Posts = 1;
        FullName = user.FullName;
        CreateTime = user.CreateTime;
        Gender = user.Gender;
        BirthDate = user.BirthDate;
    }
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public DateTime? BirthDate { get; set; }
    public Gender Gender { get; set; }
    public int? Posts { get; set; }
    public DateTime? CreateTime { get; set; }
}