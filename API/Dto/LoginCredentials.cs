using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace API.Dto;

public class LoginCredentials
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}