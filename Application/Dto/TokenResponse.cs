using System.ComponentModel.DataAnnotations;

namespace Application.Dto;
public class TokenResponse
{
    public TokenResponse(string token)
    {
        Token = token;
    }

    [MinLength(1)]
    public string Token { get; set; }
}
