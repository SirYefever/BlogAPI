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
[ApiExplorerSettings(GroupName = "Comments")]
[Authorize]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [SwaggerOperation("Get all nested comments(replies)")]
    [AllowAnonymous]
    [HttpGet("api/comment/{id}/tree")]
    [SwaggerResponse(200, "Success", Type = typeof(List<CommentDto>))]
    [SwaggerResponse(400, "BadRequest")]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(500, "Internal Server Error", Type = typeof(Response))]
    public async Task<IActionResult> GetReplies(Guid id)
    {
        List<Comment> replies = new();
        replies = await _commentService.GetReplies(id);
        var repliesDto = replies.Select(r => new CommentDto(r));
        return Ok(repliesDto);
    }

    [SwaggerOperation("Add a comment to a concrete post")]
    [HttpPost("api/post/{id}/comment")]
    [SwaggerResponse(200, "Success", Type = typeof(CreateCommentDto))]
    [SwaggerResponse(400, "BadRequest")]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(403, "Forbidden")]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(500, "Internal Server Error", Type = typeof(Response))]
    public async Task<IActionResult> AddCommentToPost(Guid id, [FromBody] CreateCommentDto model)
    {
        var userId = Guid.Empty;
        Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
        var comment = CommentConverter.CreateCommentDtoToComment(model);
        comment.AuthorId = userId;
        comment.Author = HttpContext.User.Identity.Name;
        comment.CreateTime = DateTime.UtcNow;

        await _commentService.CreateCommentAsync(id, comment);
        return Ok();
    }

    [SwaggerOperation("Edit concrete comment")]
    [HttpPut("api/comment/{id}")]
    [SwaggerResponse(200, "Success")]
    [SwaggerResponse(400, "BadRequest")]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(500, "Internal Server Error", Type = typeof(Response))]
    public async Task<IActionResult> EditByIdAsync(Guid id, [FromBody] UpdateCommentDto dto)
    {
        var userId = Guid.Empty;
        Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
        await _commentService.UpdateCommentAsync(id, dto.Content, userId);
        return Ok();
    }

    [SwaggerOperation("Delete concrete comment")]
    [HttpDelete("api/comment/{id}")]
    [SwaggerResponse(200, "Success")]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(403, "Forbidden")]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(500, "Internal Server Error", Type = typeof(Response))]
    public async Task<IActionResult> DeleteByIdAsync(Guid id)
    {
        var userId = Guid.Empty;
        Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
        await _commentService.DeleteCommentAsync(id, userId);
        return Ok();
    }
}