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
    [AllowAnonymous]
    [HttpGet("api/comment/{id}/tree")]
    [SwaggerResponse(statusCode: 200, description: "Success", Type = typeof(List<CommentDto>))]
    [SwaggerResponse(statusCode:400, description: "BadRequest")]
    [SwaggerResponse(statusCode:404, description: "Not Found")]
    [SwaggerResponse(statusCode:500, description: "Internal Server Error", Type = typeof(Response))]
    public async Task<IActionResult> GetReplies(Guid id)
    {
        List<Comment> replies = new List<Comment>();
        replies = await _commentService.GetReplies(id);
        var repliesDto = replies.Select(r => new CommentDto(r));
        return Ok(repliesDto);
    }

    [SwaggerOperation("Add a comment to a concrete post")]
    [HttpPost("api/post/{id}/comment")]
    [SwaggerResponse(statusCode: 200, description: "Success", Type = typeof(CreateCommentDto))]
    [SwaggerResponse(statusCode:400, description: "BadRequest")]
    [SwaggerResponse(statusCode:401, description: "Unauthorized")]
    [SwaggerResponse(statusCode:403, description: "Forbidden")]
    [SwaggerResponse(statusCode:404, description: "Not Found")]
    [SwaggerResponse(statusCode:500, description: "Internal Server Error", Type = typeof(Response))]
    public async Task<IActionResult> AddCommentToPost(Guid id, [FromBody]CreateCommentDto model)
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
    [SwaggerResponse(statusCode: 200, description: "Success")]
    [SwaggerResponse(statusCode:400, description: "BadRequest")]
    [SwaggerResponse(statusCode:404, description: "Not Found")]
    [SwaggerResponse(statusCode:500, description: "Internal Server Error", Type = typeof(Response))]
    public async Task<IActionResult> EditByIdAsync(Guid id, [FromBody]UpdateCommentDto dto)
    {
        var userId = Guid.Empty;
        Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
        await _commentService.UpdateCommentAsync(id, dto.Content, userId);
        return Ok();
    }

    [SwaggerOperation("Delete concrete comment")]
    [HttpDelete("api/comment/{id}")]
    [SwaggerResponse(statusCode: 200, description: "Success")]
    [SwaggerResponse(statusCode:401, description: "Unauthorized")]
    [SwaggerResponse(statusCode:403, description: "Forbidden")]
    [SwaggerResponse(statusCode:404, description: "Not Found")]
    [SwaggerResponse(statusCode:500, description: "Internal Server Error", Type = typeof(Response))]    public async Task<IActionResult> DeleteByIdAsync(Guid id)
    {
        var userId = Guid.Empty;
        Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
        await _commentService.DeleteCommentAsync(id, userId);
        return Ok();
    }
}