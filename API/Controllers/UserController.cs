using API.Dto;
using API.Converters;
using Application.Dto;
using Core.Models;
using Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

//TODO: remove the default "/" endpoint somehow
public class UserController
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    // TODO: endpoint functions have to get Dto objects as input
    [HttpPost("api/account/register")]
    public async Task<ActionResult<TokenResponse>> Register([FromBody]UserRegisterModel model)
    {
        //Call service, do mapping, return
        User user = UserConverters.UserRegisterModelToUser(model); 
        _userService.CreateUser(user);
        
        // TODO: Dedicate weather this is the way to validate property's length
        // Validate fullName length and phoneNumberLength 
        if (user.FullName.Length < 1 || user.PhoneNumber.Length < 1) 
            return null; // TODO: Determine how to handle this case (need swagger to return corresponding code)
        
        //call service
        user = await _userService.CreateUser(user);
        TokenResponse tokenResponse = new TokenResponse(user.Token);
        return tokenResponse;
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