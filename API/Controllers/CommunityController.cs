using System.ComponentModel;
using System.Security.Claims;
using API.Converters;
using API.Dto;
using Core.InterfaceContracts;
using Core.Models;
using Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
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
    private readonly PostConverters _postConverters;

    public CommunityController(ICommunityService communityService, IUserCommunityService userCommunityService, IPostService postService, PostConverters postConverters)
    {
        _communityService = communityService;
        _userCommunityService = userCommunityService;
        _postService = postService;
        _postConverters = postConverters;
    }

    [SwaggerOperation("Get community list")]
    [HttpGet("api/community")]
    [AllowAnonymous]
    [SwaggerResponse(statusCode: 200, description: "Success", Type = typeof(List<CommunityDto>))]
    [SwaggerResponse(statusCode:500, description: "Internal Server Error", Type = typeof(Response))]
    public async Task<IActionResult> GetCommunityList()
    {
        var communitylist = await _communityService.GetCommunities();
        return Ok(communitylist);
    }
    
    [SwaggerOperation("Get user's community list (with the greatest user's role in the community)")]
    [HttpGet("api/community/my")]
    [SwaggerResponse(statusCode: 200, description: "Success", Type = typeof(List<CommunityUserDto>))]
    [SwaggerResponse(statusCode:401, description: "Unauthorized")]
    [SwaggerResponse(statusCode:404, description: "Not Found")]
    [SwaggerResponse(statusCode:500, description: "Internal Server Error", Type = typeof(Response))]
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
    [SwaggerResponse(statusCode: 200, description: "Success", Type = typeof(CommunityFullDto))]
    [SwaggerResponse(statusCode:404, description: "Not Found")]
    [SwaggerResponse(statusCode:500, description: "Internal Server Error", Type = typeof(Response))]
    public async Task<IActionResult> GetCommunityInfoById(Guid id)
    {
        var communityFullDto =
            CommunityConverters.CommunityToCommunityFullDto(await _communityService.GetCommunityById(id));
        
        communityFullDto.SubscribersCount = await _communityService.GetSubscriberCountByCommunityId(id);
        return Ok(communityFullDto);
    }
    
    
    [SwaggerOperation("Get community's posts")]
    [HttpGet("api/community/{id}/post")]
    [SwaggerResponse(statusCode: 200, description: "Success", Type = typeof(PostPagesListDto))]
    [SwaggerResponse(statusCode:400, description: "BadRequest")]
    [SwaggerResponse(statusCode:401, description: "Unauthorized")]
    [SwaggerResponse(statusCode:403, description: "Forbidden")]
    [SwaggerResponse(statusCode:404, description: "Not Found")]
    [SwaggerResponse(statusCode:500, description: "Internal Server Error", Type = typeof(Response))]
    public async Task<IActionResult> GetPostsOfCommunity(Guid id, [FromQuery]List<Guid>? tags, PostSorting? sorting,
        int? page = 1, int? size = 5)
    {
        var curUserId = Guid.Empty;
        Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out curUserId);
        
        var request = new CommunityPostListRequest();
        request.CommunityId = id;
        request.TagGuids = tags;
        request.Sorting = sorting;
        request.Page = page;
        request.PageSize = size;
        var posts = await _communityService.GetPostsOfCommunity(request, curUserId);
        var postDtoList = new List<PostDto>();
        
        foreach (var post in posts)
            postDtoList.Add(await _postConverters.PostToPostDto(post));
        
        var result = new PostPagesListDto();
        result.Posts = postDtoList;
        result.Pagination = new PageInfoModel();
        result.Pagination.Size = postDtoList.Count;
        result.Pagination.Count = await _communityService.GetPostQuantity(id);
        result.Pagination.Current = (int)page;
        return Ok(result);
    }
    
    [SwaggerOperation("Create a post in the specified community")]
    [HttpPost("api/community/{id}/post")]
    [SwaggerResponse(statusCode: 200, description: "Success", Type = typeof(Guid))]
    [SwaggerResponse(statusCode:400, description: "BadRequest")]
    [SwaggerResponse(statusCode:401, description: "Unauthorized")]
    [SwaggerResponse(statusCode:403, description: "Forbidden")]
    [SwaggerResponse(statusCode:404, description: "Not Found")]
    [SwaggerResponse(statusCode:500, description: "Internal Server Error", Type = typeof(Response))]
    public async Task<IActionResult> CreatePost(Guid id, [FromBody] CreatePostDto createPostDto)
    {
        var post = PostConverters.CreatePostDtoToPost(createPostDto, id);
        post.Author = HttpContext.User.Identity.Name;
        var curUserId = Guid.Empty;
        Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out curUserId);
        post.AuthorId = curUserId;
        await _postService.CreatePost(post, createPostDto.Tags);
        return Ok(post.Id);
    }
    
    [SwaggerOperation("Get the greatest user's role in the community (or null if the user is not a member of " +
                      "the community)")]
    [HttpGet("api/community/{id}/role")]
    [SwaggerResponse(statusCode: 200, description: "Success", Type = typeof(CommunityRole))]
    [SwaggerResponse(statusCode:401, description: "Unauthorized")]
    [SwaggerResponse(statusCode:404, description: "Not Found")]
    [SwaggerResponse(statusCode:500, description: "Internal Server Error", Type = typeof(Response))]
    public async Task<IActionResult> GetRoleByCommunityIdAsync(Guid id)
    {
        var userId = Guid.Empty;
        Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
        if (! await _userCommunityService.IsUserInTheCommunity(id, userId))
            return new JsonResult(null);
        
        var role = await _userCommunityService.GetHighestRoleOfUserInCommunity(id, userId);
        return Ok(role);
    }

    [SwaggerOperation("Subscribe a user to the community")]
    [HttpPost("api/community/{id}/subscribe")]
    [SwaggerResponse(statusCode: 200, description: "Success")]
    [SwaggerResponse(statusCode:401, description: "Unauthorized")]
    [SwaggerResponse(statusCode:404, description: "Not Found")]
    [SwaggerResponse(statusCode:500, description: "Internal Server Error", Type = typeof(Response))]
    public async Task<IActionResult> Subscribe(Guid id)
    {
        var userId = Guid.Empty;
        Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
        await _userCommunityService.AddUserToTheCommunity(userId, id, CommunityRole.Subscriber);
        return Ok();
    }
    [SwaggerOperation("Unsubscribe a user from the community")]
    [HttpDelete("api/community/{id}/unsubscribe")]
    [SwaggerResponse(statusCode: 200, description: "Success")]
    [SwaggerResponse(statusCode:400, description: "BadRequest")]
    [SwaggerResponse(statusCode:401, description: "Unauthorized")]
    [SwaggerResponse(statusCode:404, description: "Not Found")]
    [SwaggerResponse(statusCode:500, description: "Internal Server Error", Type = typeof(Response))] 
    public async Task<IActionResult> Unsubscribe(Guid id)
    {
        var userId = Guid.Empty;
        Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
        await _userCommunityService.UnsubscribeUserToCommunityAsync(userId, id);
        return Ok();
    }

    [SwaggerOperation("Create new community")]
    [HttpPost("api/community/create")]
    [SwaggerResponse(statusCode: 200, description: "Success", Type = typeof(Guid))]
    [SwaggerResponse(statusCode:400, description: "BadRequest")]
    [SwaggerResponse(statusCode:401, description: "Unauthorized")]
    [SwaggerResponse(statusCode:500, description: "Internal Server Error", Type = typeof(Response))] 
    public async Task<IActionResult> CreateCommunity([FromBody]CreateCommunityDto createCommunityDto)
    {
        var community = CommunityConverters.CreateCommunityDtoToCommunity(createCommunityDto);
        community = await _communityService.CreateCommunityAsync(community);
        var userId = Guid.Empty;
        Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
        await _userCommunityService.AddUserToTheCommunity(community.Id, userId, CommunityRole.Administrator);
        return Ok(community.Id);
    }
}