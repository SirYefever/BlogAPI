using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace API;

public static class ApiExtensions
{
    public static void AddApiAutentication(this IServiceCollection services, JwtOptions jwtOptions)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddScheme<JwtBearerOptions,
                CustomJwtBearerHandler>(JwtBearerDefaults.AuthenticationScheme, options => { });
    }
}