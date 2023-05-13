using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Duende.IdentityServer.Stores.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Common.Helpers;
using Microsoft.IdentityModel.Tokens;
using ProFilePOC2.Application.Common.Interfaces;
using ProFilePOC2.Domain.Entities;

namespace WebUI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IdentityController : ControllerBase
{
    private readonly IApplicationDbContext _context;

    public IdentityController(IApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel request)
    {
        var account = await _context.Users.FirstOrDefaultAsync(a => a.Username.Equals(request.UserName));
        if (account is null)
        {
            return Unauthorized();
        }

        if (!account.PasswordHash.Equals(SecurityUtil.Hash(request.Password)))
        {
            return Unauthorized();
        }

        var accessToken = GenerateAccessToken(account);

        return Ok(new { Token = accessToken.token, expiryDate = accessToken.expire });
    }
    
    private (string token, DateTime expire) GenerateAccessToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("asfafasdljkfadjklsdafkjasdflkjsfadlkasdlkjfadjklasfkdjklsadfkj");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Name, user.Username),
                new(ClaimTypes.Email, user.Email)
                // Add additional claims as needed
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return (tokenHandler.WriteToken(token), token.ValidTo);
    }
}