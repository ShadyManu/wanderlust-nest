using Application.Commons.Result;

namespace Application.Commons.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);
    Task<bool> IsInRoleAsync(string userId, string role);
    Task<bool> AuthorizeAsync(string userId, string policyName);
    // Task<(Result<> result, string UserId)> CreateUserAsync(string userName, string password);
    Task<bool> DeleteUserAsync(string userId);
}