using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Grpc.Core;

namespace UserService.Services;

public class UserApiService:User.UserBase
{
    private ReturnerUser  _returnerUser;
    private ILogger<UserApiService> _logger;
    private MyDbContext _db;
    public UserApiService(ILogger<UserApiService> logger, MyDbContext db)
    {
        _logger = logger;
        _db = db;
        _returnerUser = new ReturnerUser(db);
    }

    public override Task<Response> Me(Request request, ServerCallContext context)
    {
        var metadata = context.RequestHeaders;
        var response = new Response();
        try
        {
            response.User = _returnerUser.ReturnUserInfoFromJwt( metadata.GetValue("Authorization").ToString());
            response.Message = "OK";
            response.Status = Statuses.Ok;
        }
        catch (Exception exception)
        {
            response.Message = exception.Message;
            response.Status = Statuses.Unknown;
        }
        return Task.FromResult(response);
    }

    public override Task<Response> ChangeUserInfo(Request request, ServerCallContext context)
    {
        var metadata = context.RequestHeaders;
        var response = new Response();
        try
        {
            var jwt = metadata.GetValue("Authorization").ToString();
            var user = _returnerUser.ReturnUserFromJwt(jwt);
            user.Name = request.NewUserInfo.Name;
            user.Surname = request.NewUserInfo.Surname;
            user.IdProfession = _db.Professions.First(x => x.Title == request.NewUserInfo.Profession).Id;
            user.Phone = request.NewUserInfo.Phone;
            _db.Users.Update(user);
            _db.SaveChanges();
            //cahnge userInfo
            response.Message = "OK";
            response.Status = Statuses.Ok;
        }
        catch (Exception exception)
        {
            response.Message = exception.Message;
            response.Status = Statuses.Unknown;
        }
        return Task.FromResult(response);
    }


    public override Task<Response> ChangePassword(Request request, ServerCallContext context)
    {
        var metadata = context.RequestHeaders;
        var response = new Response();
        try
        {
            var jwt = metadata.GetValue("Authorization").ToString();
            var user = _returnerUser.ReturnUserFromJwt(jwt);
            var loginPassword = _db.LoginPasswords.First(x => x.Id == user.IdLoginPassword);
            loginPassword.Password = request.NewPassword;
            _db.LoginPasswords.Update(loginPassword);
            _db.SaveChanges();
            response.Message = "OK";
            response.Status = Statuses.Ok;
        }
        catch (Exception exception)
        {
            response.Message = exception.Message;
            response.Status = Statuses.Unknown;
        }
        return Task.FromResult(response);
    }
} 
public class ReturnerUser
{
    private MyDbContext _db;

    public ReturnerUser(MyDbContext db)
    {
        _db = db;
    }
    public virtual UserService.Entities.User ReturnUserFromJwt(string jwtToken)
    {
        jwtToken = jwtToken.Replace("Bearer ", "");
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.ReadJwtToken(jwtToken); 
        var username = token.Claims.First(c => c.Type == ClaimTypes.Email).Value;
        var idLoginPassword = _db.LoginPasswords.First(x => x.Login == username).Id;
        var user = _db.Users.First(x => x.IdLoginPassword == idLoginPassword);
        return user;
    }

    public UserInfo ReturnUserInfoFromJwt(string jwtToken)
    {
        var user = ReturnUserFromJwt(jwtToken);
        return new UserInfo()
        {
            Name = user.Name,
            Profession = _db.Professions.First(x=>x.Id == user.IdProfession).Title,
            Phone = user.Phone,
            Surname = user.Surname
        };
    }
}