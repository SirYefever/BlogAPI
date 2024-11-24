using System.Text.Json.Serialization;
using API;
using API.Converters;
using Persistence;
using Application;
using Application.Auth;
using Core.InterfaceContracts;
using Core.ServiceContracts;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using WebApplication = Microsoft.AspNetCore.Builder.WebApplication;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.Ini;
using Microsoft.OpenApi.Models;
using Persistence;


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
// Configure swagger here
builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    }
);

builder.Services.AddEndpointsApiExplorer();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MainDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
        builder => builder.WithOrigins("localhost:44388")
            .AllowAnyHeader()
            .AllowAnyMethod());
});
            

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

//TODO: figure out what these lines do
builder.Services.AddControllers();
builder.Services.AddDbContext<MainDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserStore, UserRepository>();
builder.Services.AddTransient<ITokenGenerator, TokenService>();//TODO: figure out weather I still need this.
builder.Services.AddTransient<IJwtProvider, JwtProvider>();
builder.Services.AddTransient<IPasswordHasher, PasswordHasher>();
builder.Services.AddTransient<UserConverters>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.SwaggerDoc("Blog.API v1", new OpenApiInfo { Title = "Blog Api", Version = "1.0" });
    options.DocInclusionPredicate((docName, apiDesc) =>
    {
        var routeTemplate = apiDesc.RelativePath;
        if (routeTemplate == "/")
            return false;
        return true;
    });
});
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtSettings"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => 
    {
        options.SwaggerEndpoint("/swagger/Blog.API v1/swagger.json", "Blog.API v1");
        options.RoutePrefix = string.Empty;
    });
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => "Hello World!");
app.Run();