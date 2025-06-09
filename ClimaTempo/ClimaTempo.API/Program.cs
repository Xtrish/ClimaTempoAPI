using ClimaTempo.API.Extensions;
using ClimaTempo.API.Middlewares;
using ClimaTempo.Application.Behaviors;
using ClimaTempo.Application.Commands.Usuario.Validators;
using ClimaTempo.Domain.Interfaces;
using ClimaTempo.Infrastructure;
using ClimaTempo.Infrastructure.MediaR;
using ClimaTempo.Infrastructure.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.Text;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<IClimaRepository, WeatherApiRepository>();

builder.Services.AddDatabase(builder.Configuration);

builder.Services.AddValidatorsFromAssemblyContaining<CriarUsuarioCommandValidator>();
builder.Services.AddApplicationValidators();
builder.Services.AddProjectServices();


builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(ClimaTempo.Application.AssemblyReference).Assembly));


builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            )
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
