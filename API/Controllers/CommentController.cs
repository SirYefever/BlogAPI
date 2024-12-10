using System.Security.Claims;
using API.Converters;
using API.Dto;
using Core.Models;
using Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "Comments")]
[Authorize]
public class CommentController: ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [SwaggerOperation("Get all nested comments(replies)")]
    [HttpGet("api/comment/{id}/tree")]
    [ProducesResponseType(typeof(List<CommentDto>),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response),StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetReplies(Guid id)
    {
        List<Comment> replies = new List<Comment>();
        try
        {
            replies = await _commentService.GetReplies(id);
        }
        catch
        {
            return StatusCode(500, new Response("An internal server error occurred.",
                "Failed to get replies."));
        }
        var repliesDto = replies.Select(r => new CommentDto(r));
        return Ok(repliesDto);
    }

    [SwaggerOperation("Add a comment to a concrete post")]
    [HttpPost("api/post/{id}/comment")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddCommentToPost(Guid id, [FromBody]CreateCommentDto createCommentDto)
    {
        var userId = Guid.Empty;
        Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
        var comment = CommentConverter.CreateCommentDtoToComment(createCommentDto);
        comment.AuthorId = userId;
        comment.Author = HttpContext.User.Identity.Name;
        comment.CreateTime = DateTime.UtcNow;
        
        await _commentService.CreateCommentAsync(id, comment);
        return Ok();
    }

    [SwaggerOperation("Edit concrete comment")]
    [HttpPut("api/comment/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> EditByIdAsync(Guid id, string content)
    {
        await _commentService.UpdateCommentAsync(id, content);
        return Ok();
    }

    [SwaggerOperation("Delete concrete comment")]
    [HttpDelete("api/comment/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteByIdAsync(Guid id, string content)
    {
        await _commentService.DeleteCommentAsync(id);
        return Ok();
    }
}