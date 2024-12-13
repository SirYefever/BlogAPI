using System.Security.Claims;
using API.Converters;
using API.Dto;
using Core.Models;
using Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "Posts")]
[Authorize]
public class PostController : ControllerBase
{
    private readonly PostConverters _postConverters;
    private readonly IPostService _postService;

    public PostController(IPostService postService, PostConverters postConverters)
    {
        _postService = postService;
        _postConverters = postConverters;
    }


    [SwaggerOperation("Get a list of available posts")]
    [HttpGet("api/post")]
    [SwaggerResponse(200, "Success", Type = typeof(PostPagesListDto))]
    [SwaggerResponse(400, "BadRequest")]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(500, "Internal Server Error", Type = typeof(Response))]
    public async Task<IActionResult> GetPostList([FromQuery] List<Guid>? tags, string? author, int? min, int? max,
        PostSorting? sorting, bool onlyMyCommunities, int? page, int? size)
    {
        var request = new PostListRequest(tags, author, min, max, sorting, onlyMyCommunities, page, size);
        var curUserId = Guid.Empty;
        Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out curUserId);
        var posts = await _postService.GetAvailabePosts(request, curUserId);
        return Ok(posts);
    }

    [SwaggerOperation("Create a personal user post")]
    [HttpPost("api/post")]
    [SwaggerResponse(200, "Success", Type = typeof(Guid))]
    [SwaggerResponse(400, "BadRequest")]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(500, "Internal Server Error", Type = typeof(Response))]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostDto createPostDto)
    {
        var post = PostConverters.CreatePostDtoToPost(createPostDto, Guid.Empty);
        var curUserId = Guid.Empty;
        Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out curUserId);
        post.AuthorId = curUserId;
        post.Author = HttpContext.User.Identity.Name;
        await _postService.CreatePersonal(post, createPostDto.Tags);
        return Ok(post.Id);
    }

    [SwaggerOperation("Get information about concrete post")]
    [HttpGet("api/post/{id}")]
    [SwaggerResponse(200, "Success", Type = typeof(PostFullDto))]
    [SwaggerResponse(403, "Forbidden")]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(500, "Internal Server Error", Type = typeof(Response))]
    public async Task<IActionResult> GetInformationByIdAsync(Guid id)
    {
        var userId = Guid.Empty;
        Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
        var post = await _postService.GetPostById(id, userId);
        var postFullDto = await _postConverters.PostToPostFullDto(post);
        return Ok(postFullDto);
    }

    [SwaggerOperation("Add like to concrete post")]
    [HttpPost("api/post/{postId}/like")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerResponse(200, "Success")]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(500, "Internal Server Error", Type = typeof(Response))]
    public async Task<IActionResult> LikePost(Guid postId)
    {
        var userId = Guid.Empty;
        Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
        await _postService.LikePost(postId, userId);
        return Ok();
    }


    [SwaggerOperation("Delete like to concrete post")]
    [HttpDelete("api/post/{postId}/like")]
    [SwaggerResponse(200, "Success")]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(500, "Internal Server Error", Type = typeof(Response))]
    public async Task<IActionResult> RemoveLikeFromPost(Guid postId)
    {
        var userId = Guid.Empty;
        Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
        await _postService.UnlikePost(postId, userId);
        return Ok();
    }
}