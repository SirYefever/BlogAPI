namespace Core.Models;

public class User
{
    public User()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    public DateTime CreateTime { get; set; }
    public string? Token { get; set; }
    public string FullName { get; set; }
    public string HashedPassword { get; set; }
    public string Email { get; set; }
    public DateTime? BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string? PhoneNumber { get; set; }
}