using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using TaskService.Entities;

namespace TaskService.Services;

public class TaskApiService:Task.TaskBase
{
    private ILogger<TaskApiService> _logger;
    private MyDbContext _db;
    public TaskApiService(ILogger<TaskApiService> logger, MyDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public override Task<Response> AddExecutor(RequestData request, ServerCallContext context)
    {
        return base.AddExecutor(request, context);
    }

    public override Task<Response> CreateTask(TaskDescription request, ServerCallContext context)
    {
        return base.CreateTask(request, context);
    }

    public override Task<Response> DeleteExecutor(RequestData request, ServerCallContext context)
    {
        return base.DeleteExecutor(request, context);
    }

    public override Task<Response> DeleteTask(RequestData request, ServerCallContext context)
    {
        return base.DeleteTask(request, context);
    }

    public override Task<TaskResponse> ShowMyTask(Request request, ServerCallContext context)
    {
        return base.ShowMyTask(request, context);
    }

    public override Task<UserResponse> ShowTaskExecutors(RequestData request, ServerCallContext context)
    {
        return base.ShowTaskExecutors(request, context);
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
    
    public TaskDescription ReturnTaskDescriptions(Entities.Task task)
    {
        return new TaskDescription()
        {
            Name = task.Name,
            Description = task.Description,
            Status = _db.Statuses.First(x=>x.Id == task.IdStatus).Name,
            Author = _db.Users.First(x=>x.Id == task.IdCreator).Surname,
            Role =  _db.Professions.First(x=>x.Id == task.IdProfession).Title,
        };
    }
    //test this
    public TProto ReturnModelInTProtoType<TProto, TEntity>(TEntity entity) where TProto : class where TEntity : class
    {
        var result = Activator.CreateInstance<TProto>();
        var entityProperties = typeof(TEntity).GetProperties();
        var protoProperties = typeof(TProto).GetProperties();
        foreach (var prop in entityProperties)
        {
            var protoProperty = protoProperties.FirstOrDefault(p => p.Name == prop.Name);
            var value = prop.GetValue(entity);
            protoProperty.SetValue(result, value);
        }
        return result;
    }
}