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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody]UserRegisterModel userRegisterModel)
    {
        //Call service, do mapping, return
        var user = _userConverters.UserRegisterModelToUser(userRegisterModel); 
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        user = await _userService.CreateUser(user);
        TokenResponse tokenResponse = new TokenResponse(user.Token);
        return Ok(tokenResponse);
    }

    [SwaggerOperation("Log into the system")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("api/account/login")]
    public async Task<ActionResult<TokenResponse>> Login([FromBody] LoginCredentials creds)
    {
        //console logging should be here
        var token = await _userService.Login(creds.Email, creds.Password);
        TokenResponse tokenResponse = new TokenResponse(token);
        return tokenResponse;
    }
}