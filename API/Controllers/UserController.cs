using System.Security.Claims;
using API.Converters;
using API.Dto;
using Application.Auth;
using Application.Dto;
using Core.Models;
using Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "Users")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly UserConverters _userConverters;
    private readonly IUserService _userService;

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
    [SwaggerResponse(200, "Success", Type = typeof(TokenResponse))]
    [SwaggerResponse(400, "BadRequest")]
    [SwaggerResponse(500, "Internal Server Error", Type = typeof(Response))]
    public async Task<IActionResult> Register([FromBody] UserRegisterModel userRegisterModel)
    {
        var user = _userConverters.UserRegisterModelToUser(userRegisterModel);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        user = await _userService.CreateUser(user);
        var tokenResponse = new TokenResponse(user.Token);
        return Ok(tokenResponse);
    }

    [AllowAnonymous]
    [SwaggerOperation("Log into the system")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerResponse(200, "Success", Type = typeof(TokenResponse))]
    [SwaggerResponse(400, "BadRequest")]
    [SwaggerResponse(500, "Internal Server Error", Type = typeof(Response))]
    [HttpPost("api/account/login")]
    public async Task<ActionResult<TokenResponse>> Login([FromBody] LoginCredentials creds)
    {
        var token = await _userService.Login(creds.Email, creds.Password);
        var tokenResponse = new TokenResponse(token);
        return tokenResponse;
    }

    [SwaggerOperation("Log out system user")]
    [SwaggerResponse(200, "Success", Type = typeof(Response))]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(500, "Internal Server Error", Type = typeof(Response))]
    [HttpPost("api/account/logout")]
    public async Task<IActionResult> Logout()
    {
        var curUserId = Guid.Empty;
        Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out curUserId);
        await _userService.Logout(curUserId);
        return Ok();
    }

    [SwaggerOperation("Get user profile")]
    [SwaggerResponse(200, "Success", Type = typeof(UserDto))]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(500, "Internal Server Error", Type = typeof(Response))]
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
    [SwaggerResponse(200, "Success")]
    [SwaggerResponse(400, "BadRequest")]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(500, "Internal Server Error", Type = typeof(Response))]
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