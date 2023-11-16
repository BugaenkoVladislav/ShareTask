using Microsoft.AspNetCore.Mvc;
using ShareTaskAPI.Context;
using ShareTaskAPI.Entities;

namespace ShareTaskAPI.Service;

public class AccountActions : ControllerBase
{
    private MyDbContext _db;

    public AccountActions(MyDbContext db)
    {
        _db = db;
    }

    public static User ReturnUserFromCookie(HttpContext context, MyDbContext db)
    {
        var username = context.User.Claims.FirstOrDefault(x => x.Type == "username")?.Value;
        var user = db.Users.First(e => e.Username == username);
        return user;
    }

    public static long ReturnListId(HttpContext context)
    {
        context.Request.Cookies.TryGetValue("idList", out string? id);
        return Convert.ToInt64(id);
    }
    
}