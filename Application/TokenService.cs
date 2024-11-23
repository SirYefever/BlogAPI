using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Application;

public class TokenService: ITokenGenerator// TODO: find out weather there is a need to make an interface for this class 
{
    public string GenerateToken(string email)
    {
        // Create user claims
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, email), //TODO: Dedicate which claims we need
            // Add additional claims as needed (e.g., roles, etc.)
        };

        // TODO: Figure out where to take issuer and audience from 
        // Create a JWT
        var token = new JwtSecurityToken(
            issuer: "localhost",
            audience: "localhost",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(1440), // Token expiration time TODO: change to one hour
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("qwertyqwertyqwertyqwertyqwertyqwertyqwertyqwertyqwerty")),
                SecurityAlgorithms.HmacSha256)
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

}