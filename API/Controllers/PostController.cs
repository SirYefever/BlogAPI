using System.Security.Claims;
using Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using API.Dto;
using API.Converters;
using API.Filters;
using Application.Dto;
using Application.Auth;
using Core.Models;
using Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "Posts")]
[Authorize]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;
    private readonly PostConverters _postConverters;

    public PostController(IPostService postService, PostConverters postConverters)
    {
        _postService = postService;
        _postConverters = postConverters;
    }

    [SwaggerOperation("Create a personal user post")]
    [HttpPost("api/post")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostDto createPostDto)
    {
        var post = await _postConverters.CreatePostDtoToPost(createPostDto);
        var curUserId = Guid.Empty;
        Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out curUserId);
        post.AuthorId = curUserId;
        
        await _postService.CreatePost(post);
        return Ok(post.Id);
    }

    [SwaggerOperation("Get a list of available posts")]
    [HttpGet("api/post")]
    [GetPosts]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPostList([FromQuery]PostListRequest request)//TODO: figure out why Tags are displayed differently in swagger.
    {
        Console.WriteLine("User Id: " + User.FindFirstValue(ClaimTypes.NameIdentifier));
        _postService.GetAvailabePosts(request);
        return Ok();
    }

}