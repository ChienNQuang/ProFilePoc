using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Common.Helpers;
using ProFilePOC2.Application.Common.Interfaces;
using ProFilePOC2.Application.Common.Models;
using ProFilePOC2.Domain.Entities;

namespace ProFilePOC2.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly IApplicationDbContext _context;
    private readonly IAuthorizationService _authorizationService;

    public IdentityService(IApplicationDbContext context, IAuthorizationService authorizationService)
    {
        _context = context;
        _authorizationService = authorizationService;
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var account = await _context.Users.FirstOrDefaultAsync(a => a.Id == Guid.Parse(userId));
        return account?.Username;
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var account = await _context.Users.FirstOrDefaultAsync(a => a.Id == Guid.Parse(userId));
        return account != null && account.Role.Name.Equals(role);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = _context.Users.SingleOrDefault(u => u.Id == Guid.Parse(userId));

        if (user == null)
        {
            return false;
        }


        var principal = new ClaimsPrincipal(
            new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Name, user.Username),
                new(ClaimTypes.Email, user.Email)
            }));
        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    // public async Task<(Result Result, string UserId)> CreateUserAsync(string username, string password)
    // {
    //     var user = new Account()
    //     {
    //         Username = username,
    //         Email = username,
    //         PasswordHash = SecurityUtil.Hash(password)
    //     };
    //
    //     var result = await _context.Accounts.AddAsync(user);
    //
    //     return (result.Entity is not null ? Result.Success() : Result.Failure(e), user.Id.ToString());
    // }

    // public async Task<Result> DeleteUserAsync(string userId)
    // {
    //     var user = _context.Accounts.SingleOrDefault(u => u.Id == Guid.Parse(userId));
    //
    //     return user != null ? await DeleteUserAsync(user) : Result.Success();
    // }
    //
    // public async Task<Result> DeleteUserAsync(Account user)
    // {
    //     var result = _context.Accounts.Remove(user);
    //
    //     return result.ToApplicationResult();
    // }
}