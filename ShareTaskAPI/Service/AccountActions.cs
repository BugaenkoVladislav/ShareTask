using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        var usernameJwt = context.Request.Headers["Authorized"].ToString();
        var usernameCookie = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        if (usernameJwt.Length != 0)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(usernameJwt);//считываем токен
            var claim = token.Claims.First(c => c.Type == ClaimTypes.Name).Value; //достаем из токена claim
            var userJwt = db.Users.First(e => e.Username == claim );
            return userJwt;
        }
        var userCookie = db.Users.First(e => e.Username == usernameCookie );
        return userCookie;

    }
    
    
}