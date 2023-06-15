using Microsoft.Extensions.Options;
using ReservationService.Handler;
using ReservationService.Mapper;
using ReservationService.Middleware;
using ReservationService.Repository;
using ReservationService.Service;
using ReservationService.Settings;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc(options =>
{
    options.Interceptors.Add<ErrorHandlingInterceptor>();
});

builder.Services.AddScoped<IReservationService, ReservationService.Service.ReservationService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
builder.Services.AddSingleton<IMongoDbSettings>(serviceProvider =>
       serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);
builder.Services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));

builder.Services.AddAuthorization();
builder.Services.AddAutoMapper(typeof(MappingProfile));
var app = builder.Build();


app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<ReservationHandler>();
});

app.Run();
