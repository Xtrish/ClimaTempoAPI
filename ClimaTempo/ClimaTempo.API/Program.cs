using ClimaTempo.API.Data.Data;
using ClimaTempo.API.Domain.Interfaces;
using ClimaTempo.API.Repositories;
using ClimaTempo.API.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<IClimaRepository, WeatherApiRepository>();

builder.Services.AddProjectServices();
builder.Services.AddMediatRServices();
builder.Services.AddDatabase(builder.Configuration);

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
