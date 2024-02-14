using AuthorizeService;
using Grpc.Net.Client;


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
