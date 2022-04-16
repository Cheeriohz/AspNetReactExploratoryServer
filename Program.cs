var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
        .AddEndpointsApiExplorer()
        .AddSwaggerGen()
        .AddSingleton<AspnetCoreBackendExploratory.WeatherCacheService>()
        .AddHostedService<AspnetCoreBackendExploratory.WeatherCacheService>(provider => provider.GetService<AspnetCoreBackendExploratory.WeatherCacheService>())
        .AddSingleton(new AspnetCoreBackendExploratory.Services.GeoCodeForwardingService(builder.Configuration["ApiKeys:PositionStack"]));


var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.UseSwagger()
        .UseSwaggerUI();
}

app.UseHttpsRedirection()
   .UseAuthorization();

app.MapControllers();

app.Run();
