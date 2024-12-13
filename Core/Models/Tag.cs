namespace Core.Models;

public class Tag
{
    public Tag()
    {
        Id = Guid.NewGuid();
        CreateTime = DateTime.UtcNow;
    }

    public Tag(string name)
    {
        Id = Guid.NewGuid();
        CreateTime = DateTime.UtcNow;
        Name = name;
    }

    public Guid Id { get; set; }
    public DateTime CreateTime { get; set; }
    public string Name { get; set; }
}