using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ShareTaskAPI.Service.Constraints;

public class JwtOrCookieRequirement: IAuthorizationRequirement
{
    public string? Jwt{ get; }
    public string? Cookie { get; }

    public JwtOrCookieRequirement(string jwt, string cookie)
    {
        Jwt = jwt;
        Cookie = cookie;
    }

}

public class JwtOrCookieValueHandler : AuthorizationHandler<JwtOrCookieRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, JwtOrCookieRequirement requirement)
    {
        if (context.Resource is HttpContext httpContext)
        {
            if (httpContext.User.Claims.FirstOrDefault(x=>x.Type == ClaimTypes.Name)?.Value != null || httpContext.Request.Headers[requirement.Jwt].ToString() != null)
            {
                context.Succeed(requirement);
            }
        }
        return Task.CompletedTask;
    }
}