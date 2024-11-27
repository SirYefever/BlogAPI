using API.Converters;
using API.Dto;
using Core.Models;
using Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "Communities")]
[Authorize]
public class CommunityController: ControllerBase
{
    private readonly ICommunityService _communityService;
    
    [SwaggerOperation("Create new community")]
    [HttpPost("api/community/create")]
    [ProducesResponseType(StatusCodes.Status200OK)] //TODO: figure out which responses are possible here
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateCommunity([FromBody]CreateCommunityDto createCommunityDto)
    {
        var community = CommunityConverters.CreateCommunityDtoToCommunity(createCommunityDto);
        community = await _communityService.CreateCommunityAsync(community);
        return Ok(community);
    }

    [SwaggerOperation("Get community list")]
    [HttpGet("api/community")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCommunityList()
    {
        var communitylist = await _communityService.GetCommunities();
        return Ok(communitylist);
    }
    
    [SwaggerOperation("Get information about community")]
    [HttpGet("api/community/{id}")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCommunityInfoById(Guid id)
    {
        var communityDto = CommunityConverters.CommunityToCommunityDto(await _communityService.GetCommunityById(id));
        return Ok(communityDto);
    }
}