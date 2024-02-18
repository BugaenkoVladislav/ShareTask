using AuthorizeService;
using Microsoft.EntityFrameworkCore;
using TeamService;
using TeamService.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddDbContext<MyDbContext>(options=>options.UseNpgsql(builder.Configuration.GetConnectionString("Def")));
var app = builder.Build();
app.MapGrpcService<TeamApiService>();
// Configure the HTTP request pipeline.
app.Run();