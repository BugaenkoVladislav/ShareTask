using AuthorizeService;
using Grpc.Core;
using Grpc.Net.Client;
using UserService;


var chanel = GrpcChannel.ForAddress("http://localhost:5102");
var client = new Authorize.AuthorizeClient(chanel);
var logPassword = new LoginPassword()
{

     Login = "2@gmail.com",
     Password = "1234"
};
var result = await client.SignInAsync(new LoginPassword()
{
     Login = logPassword.Login,
     Password = logPassword.Password
});
Console.WriteLine(result.Message.ToString());   


var userChanel = GrpcChannel.ForAddress("http://localhost:5234");
var userClient = new User.UserClient(userChanel);

var headers = new Metadata { { "Authorization", $"Bearer {result.Message.ToString()}" } };
var callOptions = new CallOptions(headers);

var meResult = await userClient.MeAsync(new Request(),headers);
Console.WriteLine(meResult.User.ToString());
var req = new Request()
{
     NewPassword = "1234567"
};
var changeResult = await userClient.ChangePasswordAsync(req, headers);
Console.WriteLine(changeResult.Status.ToString());
