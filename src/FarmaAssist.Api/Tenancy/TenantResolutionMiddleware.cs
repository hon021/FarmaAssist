using System.Security.Claims;
using FarmaAssist.Api.Domain;
using Microsoft.AspNetCore.Identity;

namespace FarmaAssist.Api.Tenancy;

public class TenantResolutionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, ITenantContext tenant, UserManager<ApplicationUser> users)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = userId is null ? null : await users.FindByIdAsync(userId);

            if (user is not null)
            {
                tenant.TenantId = user.TenantId;
            }
        }

        await next(context);
    }
}
