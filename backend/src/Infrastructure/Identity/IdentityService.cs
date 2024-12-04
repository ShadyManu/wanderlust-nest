using Application.Commons.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<User> _userManager;
    private readonly IUserClaimsPrincipalFactory<User> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;

    public IdentityService(UserManager<User> userManager, IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory, IAuthorizationService authorizationService)
    {
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user?.UserName;
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);
        if (user is null) return await Task.FromResult(false);
        
        return await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);
        if (user is null) return await Task.FromResult(false);
        
        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);
        var result = _authorizationService.AuthorizeAsync(principal, policyName);

        return result.IsCompletedSuccessfully;
    }

    public async Task<bool> DeleteUserAsync(string userId)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);
        
        if (user is null) return await Task.FromResult(true);
        
        return await DeleteUserAsync(user);
    }

    private async Task<bool> DeleteUserAsync(User user)
    {
        var result = await _userManager.DeleteAsync(user);
        return result.Succeeded;
    }
}