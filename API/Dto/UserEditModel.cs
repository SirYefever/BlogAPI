using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Core.Models;

namespace API.Dto;

public class UserEditModel : IValidatableObject
{
    [Required] [MaxLength(1000)] public string FullName { get; set; }

    [Required] [EmailAddress] public string Email { get; set; }

    public DateTime? BirthDate { get; set; }
    public Gender Gender { get; set; }

    [Phone] public string? PhoneNumber { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var phoneRegex = @"\+7 \(\d{3}\) \d{3}-\d{2}-\d{2}";
        List<ValidationResult> results = new();

        if (BirthDate >= DateTime.UtcNow)
            results.Add(new ValidationResult("Birthdate is invalid", new[] { nameof(BirthDate) }));

        if (!Regex.IsMatch(PhoneNumber, phoneRegex))
            results.Add(new ValidationResult("Phone number has to match this template: +7 (xxx) xxx-xx-xx",
                new[] { nameof(PhoneNumber) }));

        return results;
    }
}
//TODO: password validation: requires at least one digit