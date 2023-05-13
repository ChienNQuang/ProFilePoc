using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProFilePOC2.Application.Accounts.Commands.CreateAccount;
using ProFilePOC2.Application.Common.Models;
using ProFilePOC2.Application.Common.Models.Dtos;
using WebUI.Controllers.Payload;

namespace WebUI.Controllers;

public class AccountsController : ApiControllerBase
{

    [HttpPost("create-account")]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountCommand command)
    {
        // var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName.Equals(command.Username));
        // if (user is not null)
        // {
        //     return Conflict();
        // }
        var result = await Mediator.Send(command);
        return Ok(ApiResponse<UserDto>.Succeed(result));
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        // var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: false, lockoutOnFailure: false);
        //
        // if (result.Succeeded)
        // {
        //     var user = await _userManager.FindByNameAsync(model.UserName);
        //
        //     // Generate an access token using IdentityServer
        //     var accessToken = GenerateAccessToken(user);
        //
        //     // Return the access token in the response
        //     return Ok(new
        //     {
        //         AccessToken = accessToken.token,
        //         Expire = accessToken.expire
        //     });
        // }
        //
        // // Login failed, return an appropriate error response
        return Unauthorized();
    }
    
    
}