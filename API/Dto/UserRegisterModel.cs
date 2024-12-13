using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Core.Models;

namespace API.Dto;

public class UserRegisterModel : IValidatableObject
{
    [Required] [MaxLength(1000)] public string FullName { get; set; }

    [MinLength(6)] public string Password { get; set; }

    [Required] [EmailAddress] public string Email { get; set; }

    public DateTime? BirthDate { get; set; }
    public Gender Gender { get; set; }

    [Phone] public string? PhoneNumber { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var phoneRegex = @"(\+7|8)( |-|)(\d{3})( |-|)(\d{3})( |-|)(\d{2})( |-|)(\d{2})";
        List<ValidationResult> results = new();

        if (BirthDate >= DateTime.UtcNow)
            results.Add(new ValidationResult("Birthdate is invalid", new[] { nameof(BirthDate) }));

        if (!Regex.IsMatch(PhoneNumber, phoneRegex))
            results.Add(new ValidationResult(
                "Phone number has to match this regex: (\\+7|8)( |-|)(\\d{3})( |-|)(\\d{3})( |-|)(\\d{2})( |-|)(\\d{2})",
                new[] { nameof(PhoneNumber) }));

        if (!Password.ToList().Intersect("01234567890".ToList()).Any())
            results.Add(new ValidationResult("Password has to contain at least one digit."));

        return results;
    }
}