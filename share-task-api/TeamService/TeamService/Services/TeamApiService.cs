using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Grpc.Core;
using TeamService.Entities;

namespace TeamService.Services;

public class TeamApiService:Team.TeamBase
{
    private Returner  _returner;
    private ILogger<TeamApiService> _logger;
    private MyDbContext _db;
    public TeamApiService(ILogger<TeamApiService>logger, MyDbContext db)
    {
        _logger = logger;
        _db = db;
        _returner = new Returner(db);
    }


    public override Task<TeamResponse> ShowMyTeams(Request request, ServerCallContext context)
    {
        var teamResponse = new TeamResponse();
        var response = new Response();
        var metadata = context.RequestHeaders;
        try
        {
            var user = _returner.ReturnUserFromJwt(metadata.GetValue("Authorization").ToString());
            var resultId = _db.TeamsUsers.Where(x => x.IdUser == user.Id).ToList();

            foreach (var i in resultId)
            {
                var resTeam = _db.Teams.First(x => x.Id == i.IdTeam);
                var res = _returner.ReturnTeamDescriptions(resTeam);
                teamResponse.Teams.Add(res);
            }
            response.Message = "OK";
            response.Status = Status.Ok;
        }
        catch (Exception exception)
        {
            response.Message = exception.Message;
            response.Status = Status.Unknown; 
        }

        teamResponse.Response = response;
        return Task.FromResult(teamResponse);
    }

    public override Task<Response> CreateTeam(TeamDescription request, ServerCallContext context)
    {
        var response = new Response();
        var metadata = context.RequestHeaders;
        try
        {
            var user = _returner.ReturnUserFromJwt(metadata.GetValue("Authorization").ToString());
            _db.Teams.Add(new Entities.Team()
            {
                Title = request.Title,
                Description = request.Description
            });
            _db.SaveChanges();
            _db.TeamsUsers.Add(new TeamUser()
            {
                IsAdmin = true,
                IdTeam = _db.Teams.First(x=>x.Title == request.Title && x.Description == request.Description).Id,
                IdUser = user.Id
            });
            _db.SaveChanges();
            response.Message = "OK";
            response.Status = Status.Ok;

        }
        catch (Exception exception)
        {
            response.Message = exception.Message;
            response.Status = Status.Unknown;
        }
        return Task.FromResult(response);
    }

    public override Task<Response> DeleteTeam(RequestData request, ServerCallContext context)
    {
        var response = new Response();
        var metadata = context.RequestHeaders;
        try
        {
            var user = _returner.ReturnUserFromJwt(metadata.GetValue("Authorization").ToString());
            var teamWhereAdmin = _db.TeamsUsers.First(x => x.IsAdmin == true && x.IdTeam == request.IdTeam);
            if (user.Id != teamWhereAdmin.IdUser)
                throw new InvalidOperationException();
            var team = _db.Teams.First(x => x.Id == teamWhereAdmin.IdTeam);
            _db.Remove(teamWhereAdmin);
            _db.Remove(team);
            _db.SaveChanges();
            response.Message = "OK";
            response.Status = Status.Ok;
        }
        catch (Exception exception)
        {
            response.Message = exception.Message;
            response.Status = Status.Unknown;
        }
        return Task.FromResult(response);
    }

    public override Task<UserResponse> ShowUsersInTeam(RequestData request, ServerCallContext context)
    {
        var userResponse = new UserResponse();
        var response = new Response();
        var metadata = context.RequestHeaders;
        try
        {
            var user = _returner.ReturnUserFromJwt(metadata.GetValue("Authorization").ToString());
            if (_db.TeamsUsers.FirstOrDefault(x => x.IdUser == user.Id && request.IdTeam == x.IdTeam) == null)
                throw new NullReferenceException();
            var resultId = _db.TeamsUsers.Where(x => x.IdTeam == request.IdTeam).ToList();
            foreach (var i in resultId)
            {
                var resTeam = _db.Users.First(x => x.Id == i.IdUser);
                var res = _returner.ReturnUserDescriptions(resTeam);
                userResponse.Users.Add(res);
            }

            response.Message = "OK";
            response.Status = Status.Ok;
        }
        catch (NullReferenceException exception)
        {
            response.Message = exception.Message;
            response.Status = Status.Forbid; 
        }
        catch (Exception exception)
        {
            response.Message = exception.Message;
            response.Status = Status.Unknown; 
        }
        userResponse.Response = response;
        return Task.FromResult(userResponse);
    }

    public override Task<Response> AddUserInTeam(RequestData request, ServerCallContext context)
    {
        var response = new Response();
        var metadata = context.RequestHeaders;
        try
        {
            var user = _returner.ReturnUserFromJwt(metadata.GetValue("Authorization").ToString());
            var team = _db.TeamsUsers.First(x => x.IdTeam == request.IdTeam && x.IsAdmin == true);
            if (user.Id == team.IdUser)
            {
                _db.TeamsUsers.Add(new TeamUser()
                {
                    IsAdmin = false,
                    IdTeam = request.IdTeam,
                    IdUser = request.IdUser
                });
                _db.SaveChanges();
                response.Message = "OK";
                response.Status = Status.Ok;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
        catch (Exception exception)
        {
            response.Message = exception.Message;
            response.Status = Status.Unknown;
        }
        return Task.FromResult(response);
    }

    public override Task<Response> DeleteUserFromTeam(RequestData request, ServerCallContext context)
    {
        var response = new Response();
        var metadata = context.RequestHeaders;
        try
        {
            var user = _returner.ReturnUserFromJwt(metadata.GetValue("Authorization").ToString());
            var adminTeam = _db.TeamsUsers.First(x => x.IdTeam == request.IdTeam && x.IsAdmin == true).IdUser;
            if (user.Id == adminTeam)
            {
                var team = _db.TeamsUsers.First(x => x.IdTeam == request.IdTeam && x.IdUser == request.IdUser);
                _db.TeamsUsers.Remove(team);
                _db.SaveChanges();
                response.Message = "OK";
                response.Status = Status.Ok;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
        catch (Exception exception)
        {
            response.Message = exception.Message;
            response.Status = Status.Unknown;
        }
        return Task.FromResult(response);
    }
}


public class Returner
{
    private MyDbContext _db;

    public Returner(MyDbContext db)
    {
        _db = db;
    }
    public virtual Entities.User ReturnUserFromJwt(string jwtToken)
    {
        jwtToken = jwtToken.Replace("Bearer ", "");
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.ReadJwtToken(jwtToken); 
        var username = token.Claims.First(c => c.Type == ClaimTypes.Email).Value;
        var idLoginPassword = _db.LoginPasswords.First(x => x.Login == username).Id;
        var user = _db.Users.First(x => x.IdLoginPassword == idLoginPassword);
        return user;
    }

    public UserDescription ReturnUserDescriptions(User user)
    {
        return new UserDescription()
        {
            Name = user.Name,
            Profession = _db.Professions.First(x=>x.Id == user.IdProfession).Title,
            Phone = user.Phone,
            Surname = user.Surname
        };
    }
    
    public TeamDescription ReturnTeamDescriptions(Entities.Team team)
    {
        return new TeamDescription()
        {
            Title = team.Title,
            Description = team.Description,
        };
    }
}