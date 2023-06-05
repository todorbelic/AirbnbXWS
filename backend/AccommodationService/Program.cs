using AccommodationService.Handlers;
using AccommodationService.Mapper;
using AccommodationService.Middleware;
using AccommodationService.Repository;
using AccommodationService.Services;
using AccommodationService.Settings;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc(options =>
    { options.Interceptors.Add<ErrorHandlingInterceptor>(); }
);

builder.Services.AddScoped<IAccommodationService, AppAccommodationService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
builder.Services.AddSingleton<IMongoDbSettings>(serviceProvider =>
       serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);
builder.Services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
    { endpoints.MapGrpcService<AccommodationHandler>(); }
);

// Configure the HTTP request pipeline.
//app.MapGrpcService<GreeterService>();
//.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
