using System.Text.Json.Serialization;
using API;
using API.Controllers;
using API.Converters;
using Application;
using Application.Auth;
using Core.InterfaceContracts;
using Core.ServiceContracts;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Persistence;
using Persistence.GarContext;
using WebApplication = Microsoft.AspNetCore.Builder.WebApplication;


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
var connectionStringGar = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MainDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddDbContext<GarDbContext>(options =>
    options.UseNpgsql(connectionStringGar));
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
    .AddJsonFile("appsettings.json", true, true)
    .Build();

builder.Services.AddControllers();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddTransient<CustomJwtBearerHandler>();
builder.Services.AddTransient<ITokenGenerator, TokenService>();
builder.Services.AddTransient<IJwtProvider, JwtProvider>();
builder.Services.AddTransient<IPasswordHasher, PasswordHasher>();
builder.Services.AddTransient<ITagRepository, TagRepository>();
builder.Services.AddTransient<IPostRepository, PostRepository>();
builder.Services.AddTransient<IPostService, PostService>();
builder.Services.AddTransient<ITagService, TagService>();
builder.Services.AddTransient<ICommunityService, CommunityService>();
builder.Services.AddTransient<ICommunityRepository, CommunityRepository>();
builder.Services.AddTransient<IUserCommunityService, UserCommunityService>();
builder.Services.AddTransient<IUserCommunityRepository, UserCommunityRepository>();
builder.Services.AddTransient<IAuthorRepository, AuthorRepository>();
builder.Services.AddTransient<IAuthorService, AuthorService>();
builder.Services.AddTransient<IPostTagRepository, PostTagRepository>();
builder.Services.AddTransient<IPostTagService, PostTagService>();
builder.Services.AddTransient<ICommentRepository, CommentRepository>();
builder.Services.AddTransient<ICommentService, CommentService>();
builder.Services.AddTransient<ICommunityPostRepository, CommunityPostRepository>();
builder.Services.AddTransient<IPostLikeRepository, PostLikeRepository>();
builder.Services.AddTransient<IGarRepository, GarRepository>();
builder.Services.AddTransient<IGarService, GarService>();
builder.Services.AddTransient<UserConverters>();
builder.Services.AddTransient<TagConverters>();
builder.Services.AddTransient<PostConverters>();
builder.Services.AddTransient<CommunityConverters>();
builder.Services.AddTransient<UserCommunityConverters>();
builder.Services.AddTransient<CommentConverter>();
builder.Services.AddTransient<AuthorConverters>();
builder.Services.AddTransient<GarConverter>();

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
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiIwMTkzNWRhZC03YWU4LTdiNjEtYjQzMC01Y2E5NjIzNGZkZjQiLCJleHAiOjE3MzI1NDQ3NDZ9.sGlihWp0iTf1ucMKIc-Bg9l2PW3S_kwPujQ82bvrWNU\""
    });
    options.OperationFilter<AuthResponsesOperationFilter>();
});
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtSettings"));
var jwtOptions = builder.Configuration.GetSection("JwtSettings").Get<JwtOptions>();
builder.Services.AddApiAutentication(jwtOptions);

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

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers(); //TODO: figure out what is this
app.Run();