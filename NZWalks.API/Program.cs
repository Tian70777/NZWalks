using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Repositories;
using NZWalks.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NZWalksDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString")));

//add WeatherService 
//builder.Services.AddHttpClient<WeatherService>(client =>
//{
//    client.BaseAddress = new Uri("https://dmigw.govcloud.dk/v1/forecastdata");
//    client.DefaultRequestHeaders.Add("X-Gravitee-Api-Key", "726f7f25-f557-40ab-89f0-447ae22c037d");
//});
builder.Services.AddHttpClient();
builder.Services.AddTransient<WeatherService>();

// add life time to interface and implementation
// whenever using IRegionRepository, pass the concret implementation of SQLRegionRepository
builder.Services.AddTransient<IRegionRepository, SQLRegionRepository>();

// easy to change db store
//builder.Services.AddTransient<IRegionRepository, InMemoryRegionRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
