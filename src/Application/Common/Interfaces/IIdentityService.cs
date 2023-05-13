using ProFilePOC2.Application.Common.Models;

namespace ProFilePOC2.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    // Task<(Result Result, string UserId)> CreateUserAsync(string username, string password);
    //
    // Task<Result> DeleteUserAsync(string userId);
}
