using System.Security.Claims;
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

//TODO: remove the default "/" endpoint somehow
[ApiController]
[ApiExplorerSettings(GroupName = "Users")]
[Authorize]
public class UserController: ControllerBase
{
    private readonly IUserService _userService;
    private readonly UserConverters _userConverters;
    private readonly IPasswordHasher _passwordHasher;
    public UserController(IUserService userService, IPasswordHasher passwordHasher, UserConverters userConverters)
    {
        _userService = userService;
        _passwordHasher = passwordHasher;
        _userConverters = userConverters;
    }
    // TODO: endpoint functions have to get Dto objects as input
    [SwaggerOperation("Register new user")]
    [HttpPost("api/account/register")]
    [AllowAnonymous]
    [SwaggerResponse(statusCode: 200, description: "Success", Type = typeof(TokenResponse))]
    [SwaggerResponse(statusCode:400, description: "BadRequest")]
    [SwaggerResponse(statusCode:500, description: "Internal Server Error", Type = typeof(Response))]
    public async Task<IActionResult> Register([FromBody]UserRegisterModel userRegisterModel)
    {
        var user = _userConverters.UserRegisterModelToUser(userRegisterModel); 
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        user = await _userService.CreateUser(user);
        TokenResponse tokenResponse = new TokenResponse(user.Token);
        return Ok(tokenResponse);
    }

    [AllowAnonymous]
    [SwaggerOperation("Log into the system")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerResponse(statusCode: 200, description: "Success", Type = typeof(TokenResponse))]
    [SwaggerResponse(statusCode:400, description: "BadRequest")]
    [SwaggerResponse(statusCode:500, description: "Internal Server Error", Type = typeof(Response))]
    [HttpPost("api/account/login")]
    public async Task<ActionResult<TokenResponse>> Login([FromBody] LoginCredentials creds)
    {
        var token = await _userService.Login(creds.Email, creds.Password);
        TokenResponse tokenResponse = new TokenResponse(token);
        return tokenResponse;
    }

    [SwaggerOperation("Log out system user")]
    [SwaggerResponse(statusCode: 200, description: "Success", Type = typeof(Response))]
    [SwaggerResponse(statusCode:401, description: "Unauthorized")]
    [SwaggerResponse(statusCode:500, description: "Internal Server Error", Type = typeof(Response))]
    [HttpPost("api/account/logout")]
    public async Task<IActionResult> Logout()
    {
        var curUserId = Guid.Empty;
        Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out curUserId);
        await _userService.Logout(curUserId);
        return Ok();
    }
    
    [SwaggerOperation("Get user profile")]
    [SwaggerResponse(statusCode: 200, description: "Success", Type = typeof(UserDto))]
    [SwaggerResponse(statusCode:401, description: "Unauthorized")]
    [SwaggerResponse(statusCode:500, description: "Internal Server Error", Type = typeof(Response))]
    [HttpGet("api/account/profile")]
    public async Task<IActionResult> GetProfile()
    {
        var curUserId = Guid.Empty;
        Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out curUserId);
        var user = await _userService.GetUserById(curUserId);
        var dto = UserConverters.UserToUserDto(user);
        return Ok(dto);
    }
    
    [SwaggerOperation("Edit user profile")]
    [SwaggerResponse(statusCode: 200, description: "Success")]
    [SwaggerResponse(statusCode:400, description: "BadRequest")]
    [SwaggerResponse(statusCode:401, description: "Unauthorized")]
    [SwaggerResponse(statusCode:500, description: "Internal Server Error", Type = typeof(Response))]
    [HttpPut("api/account/profile")]
    public async Task<IActionResult> EditProfile(UserEditModel dto)
    {
        var curUserId = Guid.Empty;
        Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out curUserId);
        var userToUpdate = await _userService.GetUserById(curUserId);
        await _userService.UpdateUser(curUserId, userToUpdate);
        return Ok();
    }
}