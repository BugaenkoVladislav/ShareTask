using Microsoft.AspNetCore.Authorization;

namespace ShareTaskAPI.Service.Constraints;

public class ListIdCookieRequirement: IAuthorizationRequirement
{
    public string IdList { get; }

    public ListIdCookieRequirement(string idlist)
    {
        IdList = idlist;
    }
}
public class ListIdValueHandler : AuthorizationHandler<ListIdCookieRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ListIdCookieRequirement requirement)
    {
        if (context.Resource is HttpContext httpContext)
        {
            if (httpContext.Request.Cookies.TryGetValue(requirement.IdList, out var cookieValue))
            {
                if (int.TryParse(cookieValue, out var cookieValueInt))
                {
                    context.Succeed(requirement);
                }
            }
        }
        return Task.CompletedTask;
    }
}