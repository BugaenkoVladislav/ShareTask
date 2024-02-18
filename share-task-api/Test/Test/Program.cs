using AuthorizeService;
using Grpc.Core;
using Grpc.Net.Client;
using TeamService;
using UserService;
using Request = UserService.Request;


var chanel = GrpcChannel.ForAddress("http://localhost:5102");
var client = new Authorize.AuthorizeClient(chanel);
var logPassword = new LoginPassword()
{

     Login = "2@gmail.com",
     Password = "1234567"
};
var result = await client.SignInAsync(new LoginPassword()
{
     Login = logPassword.Login,
     Password = logPassword.Password
});
Console.WriteLine(result.Message.ToString());   


var headers = new Metadata { { "Authorization", $"Bearer {result.Message.ToString()}" } };
var callOptions = new CallOptions(headers);



var teamChanel = GrpcChannel.ForAddress("http://localhost:5224");
var teamClient = new Team.TeamClient(teamChanel);

var deleteUsersInTeam = await teamClient.DeleteUserFromTeamAsync(new RequestData()
{
     IdTeam = 3,
     IdUser = 5
}, callOptions);

Console.WriteLine(deleteUsersInTeam.Message.ToString());

var showMyTeams = await teamClient.ShowMyTeamsAsync(new TeamService.Request(),callOptions);
Console.WriteLine(showMyTeams.Teams);