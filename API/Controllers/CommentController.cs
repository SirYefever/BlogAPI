using System.Security.Claims;
using API.Converters;
using API.Dto;
using Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

public class CommentController: ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [SwaggerOperation("Get all nested comments(replies)")]
    [HttpGet("api/comment/{id}/tree")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetReplies(Guid id)
    {
        var replies = await _commentService.GetReplies(id);
        return Ok(replies);
    }

    [SwaggerOperation("Add a comment to a concrete post")]
    [HttpPost("api/post/{id}/comment")] //TODO: figure out why is it /post, instead of /comment
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
}