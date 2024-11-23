using API.Dto;
using API.Converters;
using Application.Dto;
using Core.Models;
using Core.ServiceContracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

//TODO: remove the default "/" endpoint somehow
[ApiController]
[ApiExplorerSettings(GroupName = "Users")]
public class UserController: ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    // TODO: endpoint functions have to get Dto objects as input
    [SwaggerOperation("Register new user")]
    [HttpPost("api/account/register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody]UserRegisterModel userRegisterModel)
    {
        //Call service, do mapping, return
        User user = UserConverters.UserRegisterModelToUser(userRegisterModel); 
        _userService.CreateUser(user);
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        //call service
        user = await _userService.CreateUser(user);
        TokenResponse tokenResponse = new TokenResponse(user.Token);
        return Ok(tokenResponse);
    }

    [HttpPost("api/account/login")]
    public async Task<ActionResult<TokenResponse>> Login([FromBody] LoginCredentials creds)
    {
        //console logging should be here
        var user = _userService.Login(creds.Email, creds.Password);
        TokenResponse tokenResponse = new TokenResponse(user.Token);
        return tokenResponse;
    }
}