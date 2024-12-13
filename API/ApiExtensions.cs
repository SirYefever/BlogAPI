using System.Text;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace API;

public static class ApiExtensions
{
    //TODO: figure out why is it better to pass jwtOptions as IOptions<JwtOptions> and how to call this function in Program.cs in that case.
    public static void AddApiAutentication(this IServiceCollection services, JwtOptions jwtOptions)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddScheme<JwtBearerOptions, CustomJwtBearerHandler>(JwtBearerDefaults.AuthenticationScheme, options => { });
    }
    
}