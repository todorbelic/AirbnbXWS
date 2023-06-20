using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserService.Handlers;
using UserService.Mapper;
using UserService.Middleware;
using UserService.Repository;
using UserService.Service;
using UserService.Settings;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.

builder.Services.AddGrpc(options =>
{
    options.Interceptors.Add<ErrorHandlingInterceptor>();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false, // Allow all issuers
                ValidateAudience = false, // Allow all audiences
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };
        });

builder.Services.AddScoped<IAppUserService, AppUserService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
//builder.Services.AddTransient<ExceptionMiddleware>();

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
    endpoints.MapGrpcService<UserHandler>();
    endpoints.MapGrpcService<UserRatingHandler>();
    endpoints.MapGrpcService<ReservationUserHandler>();

});

// Configure the HTTP request pipeline.

app.Run();
