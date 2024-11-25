using Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using API.Dto;
using API.Converters;
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
[ApiExplorerSettings(GroupName = "Users")]
[Authorize]
public class PostController: ControllerBase
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
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreatePostAsync([FromBody] CreatePostDto createPostDto)
    {
        var post = await _postConverters.CreatePostDtoToPost(createPostDto);
        await _postService.CreatePost(post);   
        return Ok(post.Id);
    }
}