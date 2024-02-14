using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using AuthorizeService.Entities;
using Grpc.Core;
using Microsoft.IdentityModel.Tokens;

namespace AuthorizeService.Services;

public class AuthorizeApiService:Authorize.AuthorizeBase
{
    private ILogger<AuthorizeApiService> _logger;
    private MyDbContext _db;

    public AuthorizeApiService(ILogger<AuthorizeApiService> logger, MyDbContext db)
    {
        _logger = logger;
        _db = db;

    }

    public override Task<Response> SignUp(SignUpInfo request, ServerCallContext context)
    {
        try
        {
            _db.LoginPasswords.Add(new Entities.LoginPassword()
            {
                Login = request.LoginPassword.Login,
                Password = request.LoginPassword.Password
            });
            _db.SaveChanges();
            _db.Users.Add(new User()
            {
                Name = request.Name,
                Surname = request.Surname,
                IdProfession = _db.Professions.First(x => x.Title == request.Profession).Id,
                IdLoginPassword = _db.LoginPasswords.First(x => x.Login == request.LoginPassword.Login).Id,
                Phone = request.Phone
            });
            _db.SaveChanges();
            return Task.FromResult(new Response()
            {
                RegistrationStatus = RegistrationStatus.Ok,
                Message = "OK"
            });
        }
        catch (InvalidOperationException exception)
        {
            return Task.FromResult(new Response()
            {
                RegistrationStatus = RegistrationStatus.BadRequest,
                Message = exception.Message
            });
        }
        catch (Exception exception)
        {
            return Task.FromResult(new Response()
            {
                RegistrationStatus = RegistrationStatus.Unknown,
                Message = exception.Message
            });
        }
    }

    public override Task<Response> SignIn(LoginPassword request, ServerCallContext context)
    {
        try
        {
            var result = _db.LoginPasswords.First(x => x.Login == request.Login && x.Password == request.Password).Id;
            var claim = new List<Claim>{new Claim(ClaimTypes.Email, request.Login)};
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claim,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            return Task.FromResult(new Response()
            {
                RegistrationStatus = RegistrationStatus.Ok,
                Message = new JwtSecurityTokenHandler().WriteToken(jwt)
            });
        }
        catch (InvalidOperationException exception)
        {
            return Task.FromResult(new Response()
            {
                RegistrationStatus = RegistrationStatus.BadRequest,
                Message = exception.Message
            });
        }
        catch (Exception exception)
        {
            return Task.FromResult(new Response()
            {
                RegistrationStatus = RegistrationStatus.Unknown,
                Message = exception.Message
            });
        }
    }
}