using System.Security.Claims;
using Application.Commons.Interfaces;

namespace WanderlustNest.Web.Services;

public class CurrentUser(IHttpContextAccessor httpContextAccessor) : IUser
{
    public Guid? Id
    {
        get
        {
            var idValue = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(idValue, out var guid) ? guid : null;
        }
    }
}