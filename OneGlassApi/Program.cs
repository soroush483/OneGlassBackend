using OneGlassApi.Interfaces;
using OneGlassApi.Options;
using OneGlassApi.Repositories;
using OneGlassApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//services cors
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder => {
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
// Get ConncetionString From appsetting
builder.Services.Configure<DatabaseOption>(
    builder.Configuration.GetSection("Database"));
// Add Dependency injection
builder.Services.AddTransient<IWeatherService, WeatherService>();
builder.Services.AddTransient<ISalesForecast, SalesForecast>();
builder.Services.AddTransient<IAlertService, AlertService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corsapp");  
 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
