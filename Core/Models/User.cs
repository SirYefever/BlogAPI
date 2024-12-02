using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class User
{
     public User()
     {
          Id = Guid.NewGuid();
     }
     //TODO: Dedicate weather we need attributes here
     public Guid Id { get; set; }
     public DateTime CreateTime { get; set; }
     public string Token { get; set; }
     public string FullName { get; set; }
     public string HashedPassword { get; set; }
     public string Email { get; set; } //TODO: dedicate how to make it unique
     public DateTime? BirthDate { get; set; }
     public Gender Gender { get; set; } //TODO: manage property name issue
     public string? PhoneNumber { get; set; }   
}