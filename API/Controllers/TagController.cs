using API.Dto;
using Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "Tags")]
[Authorize]
public class TagController: ControllerBase
{
    public readonly ITagService _tagService;

    public TagController(ITagService tagService)
    {
        _tagService = tagService;
    }

    [SwaggerOperation("Get tag list")]
    [HttpGet("api/tag")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTagsAsync()
    {
        var tags = await _tagService.GetAllTags();
        return Ok(tags);
    }
    
    [SwaggerOperation("Create tag")]
    [HttpPost("api/tag")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateTagAsync(CreateTagDto dto)
    {
        var tag = TagConverters.CreateTagDtoToTag(dto);
        try
        {
            await _tagService.CreateTag(tag);
        }
        catch
        {
            return Conflict("Tag already exists");//TODO: Use such method in other similar cases.
        }
        return Ok();
    }
}