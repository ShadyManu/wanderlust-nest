using System.Reflection;
using Application.Commons.Exceptions;
using Application.Commons.Interfaces;
using MediatR;
using AuthorizeAttribute = Application.Commons.Security.AuthorizeAttribute;

namespace Application.Commons.Behaviors;

public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IUser _user;
    private readonly IIdentityService _identityService;

    public AuthorizationBehaviour(IUser user, IIdentityService identityService)
    {
        _user = user;
        _identityService = identityService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>().ToList();

        if (authorizeAttributes.Count == 0) return await next();
        
        if (_user is null) throw new UnauthorizedAccessException();

        var authorizeAttributesWithRoles = authorizeAttributes
            .Where(a => !string.IsNullOrWhiteSpace(a.Roles))
            .ToList();

        if (authorizeAttributesWithRoles.Count <= 0) return await next();
        
        
        var authorized = false;

        var splitRoles = authorizeAttributesWithRoles.Select(a => a.Roles.Split(','));
        foreach (var roles in splitRoles)
        {
            foreach (var role in roles)
            {
                var isInRole = await _identityService.IsInRoleAsync(_user.Id.ToString()!, role.Trim());
                if (isInRole)
                {
                    authorized = true;
                    break;
                }
            }
        }
            
        if (!authorized) throw new ForbiddenAccessException();

        return await next();
    }
}