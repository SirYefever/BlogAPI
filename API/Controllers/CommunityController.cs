using System.Security.Claims;
using API.Converters;
using API.Dto;
using Core.InterfaceContracts;
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
    private readonly IUserCommunityService _userCommunityService;

    public CommunityController(ICommunityService communityService, IUserCommunityService userCommunityService)
    {
        _communityService = communityService;
        _userCommunityService = userCommunityService;
    }

    [SwaggerOperation("Create new community")]
    [HttpPost("api/community/create")]
    [ProducesResponseType(StatusCodes.Status200OK)] //TODO: figure out which responses are possible here
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateCommunity([FromBody]CreateCommunityDto createCommunityDto)
    {
        var community = CommunityConverters.CreateCommunityDtoToCommunity(createCommunityDto);
        community = await _communityService.CreateCommunityAsync(community);
        var userId = Guid.Empty;
        Guid.TryParse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId").Value, out userId);
        await _userCommunityService.AddUserToTheCommunity(community.Id, userId, CommunityRole.Administrator);
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCommunityInfoById(Guid id)
    {
        var communityDto = CommunityConverters.CommunityToCommunityDto(await _communityService.GetCommunityById(id));
        return Ok(communityDto);
    }

    [SwaggerOperation("Subscribe a user to the community")]
    [HttpPost("api/community/{id}/subscribe")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Subscribe(Guid id)
    {
        var userId = Guid.Empty;
        Guid.TryParse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId").Value, out userId);
        await _userCommunityService.AddUserToTheCommunity(userId, id, CommunityRole.Subscriber);
        return Ok();
    }
    [SwaggerOperation("Unsubscribe a user from the community")]
    [HttpDelete("api/community/{id}/unsubscribe")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Unsubscribe(Guid id)
    {
        var userId = Guid.Empty;
        Guid.TryParse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId").Value, out userId);
        await _userCommunityService.UnsubscribeUserToCommunityAsync(userId, id);
        return Ok();
    }

    [SwaggerOperation("Get user's community list (with the greatest user's role in the community)")]
    [HttpGet("api/community/my")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCommunitiesUserBelongsTo()
    {
        var userId = Guid.Empty;
        Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
        var userCommunityList = await _userCommunityService.GetUserCommunitiesByUserIdAsync(userId);
        var communityUserDtoList = new List<CommunityUserDto>();
        foreach (var uc in userCommunityList)
        {
            communityUserDtoList.Add(UserCommunityConverters.UserCommunityToCommunityUserDto(uc));
        }
        return Ok(communityUserDtoList);
    }
}