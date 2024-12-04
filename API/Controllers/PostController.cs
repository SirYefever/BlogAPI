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
using Microsoft.EntityFrameworkCore.Query;
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


    [SwaggerOperation("Get a list of available posts")]
    [HttpGet("api/post")]
    // [GetPosts]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPostList([FromQuery]PostListRequest request)//TODO: figure out why Tags are displayed differently in swagger.
    {
        var curUserId = Guid.Empty;
        Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out curUserId);
        var posts = await _postService.GetAvailabePosts(request, curUserId);
        return Ok(posts);
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
        var post = PostConverters.CreatePostDtoToPost(createPostDto, Guid.Empty);
        var curUserId = Guid.Empty;
        Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out curUserId);
        post.AuthorId = curUserId;
        await _postService.CreatePost(post);
        return Ok(post.Id);
    }
    
    [SwaggerOperation("Get information about concrete post")]
    [HttpGet("api/post/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetInformationByIdAsync(Guid id)
    {
        var post = await _postService.GetPostById(id);
        var postDto = await _postConverters.PostToPostDto(post);
        return Ok(postDto);
    }

    [SwaggerOperation("Add like to concrete post")]
    [HttpPost("api/post/{postId}/like")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> LikePost(Guid postId)
    {
        var userId = Guid.Empty;
        Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
        await _postService.LikePost(postId, userId);
        return Ok();
    }
    
    
    [SwaggerOperation("Delete like to concrete post")]
    [HttpDelete("api/post/{postId}/like")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RemoveLikeFromPost(Guid postId)
    {
        var userId = Guid.Empty;
        Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
        await _postService.UnlikePost(postId, userId);
        return Ok();
    }
}