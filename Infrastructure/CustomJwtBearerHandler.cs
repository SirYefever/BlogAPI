using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using Core.ServiceContracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure;

public class CustomJwtBearerHandler: JwtBearerHandler
{
    public CustomJwtBearerHandler(IOptionsMonitor<JwtBearerOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IUserService userService) : base(options, logger, encoder, clock)
    {
        _userService = userService;
    }

    public CustomJwtBearerHandler(IOptionsMonitor<JwtBearerOptions> options, ILoggerFactory logger, UrlEncoder encoder, IUserService userService) : base(options, logger, encoder)
    {
        _userService = userService;
    }
    private readonly IUserService _userService; 

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var endpoint = Context.GetEndpoint();
        if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
        {
            return AuthenticateResult.NoResult();
        }
        // Get the token from the Authorization header
        if (!Context.Request.Headers.TryGetValue("Authorization", out var authorizationHeaderValues))
        {
            return AuthenticateResult.Fail("Authorization header not found.");
        }

        var authorizationHeader = authorizationHeaderValues.FirstOrDefault();
        if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
        {
            return AuthenticateResult.Fail("Bearer token not found in Authorization header.");
        }

        var token = authorizationHeader.Substring("Bearer ".Length).Trim();


        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(token);
        var expirationTime = jwtSecurityToken.ValidTo;
        if (expirationTime < DateTime.UtcNow)
        {
            return AuthenticateResult.Fail("Token expired.");
        }
        
        
        
        
        
        // Set the authentication result with the claims from the API response          
        var principal = GetClaims(token);
        
        var userId = Guid.Empty;
        var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
        Guid.TryParse(userIdClaim.Value, out userId);
        var user = await _userService.GetUserById(userId);

        if (user.Token != token)
        {
            return AuthenticateResult.Fail("Invalid token.");
        }

        return AuthenticateResult.Success(new AuthenticationTicket(principal, "CustomJwtBearer"));
    }


    private ClaimsPrincipal GetClaims(string Token)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadToken(Token) as JwtSecurityToken;

        var claimsIdentity = new ClaimsIdentity(token.Claims, "Token");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        return claimsPrincipal;
    }
}