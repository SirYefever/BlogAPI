using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Application;

public class TokenService : ITokenGenerator
{
    public string GenerateToken(string email)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, email)
        };

        var token = new JwtSecurityToken(
            "localhost",
            "localhost",
            claims,
            expires: DateTime.UtcNow.AddMinutes(120),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes("qwertyqwertyqwertyqwertyqwertyqwertyqwertyqwertyqwerty")),
                SecurityAlgorithms.HmacSha256)
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
}