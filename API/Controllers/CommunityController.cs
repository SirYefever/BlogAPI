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
    private readonly IPostService _postService;

    public CommunityController(ICommunityService communityService, IUserCommunityService userCommunityService, IPostService postService)
    {
        _communityService = communityService;
        _userCommunityService = userCommunityService;
        _postService = postService;
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
    
    [SwaggerOperation("Get information about community")]
    [HttpGet("api/community/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCommunityInfoById(Guid id)
    {
        var communityDto = CommunityConverters.CommunityToCommunityDto(await _communityService.GetCommunityById(id));
        communityDto.SubscribersCount = await _communityService.GetSubscriberCountByCommunityId(id);
        return Ok(communityDto);
    }
    
    
    [SwaggerOperation("Get community's posts")]
    [HttpGet("api/community/{id}/post")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    // public async Task<IActionResult> GetPostsOfCommunity([FromQuery]CommunityPostListRequest request)
    public async Task<IActionResult> GetPostsOfCommunity(Guid id, [FromQuery]List<Guid>? tagGuids, PostSorting? sorting, int? page, int? pageSize)
    {
        var request = new CommunityPostListRequest();
        request.CommunityId = id;
        request.TagGuids = tagGuids;
        request.Sorting = sorting;
        request.Page = page;
        request.PageSize = pageSize;
        var posts = await _communityService.GetPostsOfCommunity(request);
        return Ok(posts);
    }
    
    [SwaggerOperation("Create a post in the specified community")]
    [HttpPost("api/community/{id}/post")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreatePost(Guid id, [FromBody] CreatePostDto createPostDto)
    {
        var post = PostConverters.CreatePostDtoToPost(createPostDto, id);
        var curUserId = Guid.Empty;
        Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out curUserId);
        post.AuthorId = curUserId;
        await _postService.CreatePost(post);
        return Ok(post.Id);
    }
    
    [SwaggerOperation("Get the greatest user's role in the community (or null if the user is not a member of " +
                      "the community)")]
    [HttpGet("api/community/{id}/role")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetRoleByCommunityIdAsync(Guid id)
    {
        var userId = Guid.Empty;
        Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
        try
        {
            var role = await _userCommunityService.GetHighestRoleOfUserInCommunity(id, userId);
            return Ok(role);
        }
        catch
        {
            return new JsonResult(null);
        }
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
        Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
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
        Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
        await _userCommunityService.UnsubscribeUserToCommunityAsync(userId, id);
        return Ok();
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
        Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
        await _userCommunityService.AddUserToTheCommunity(community.Id, userId, CommunityRole.Administrator);
        return Ok(community);
    }
}