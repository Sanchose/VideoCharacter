using Microsoft.AspNetCore.OpenApi;
using Scalar.AspNetCore;
using VideoCharacter.Controllers;
using Microsoft.EntityFrameworkCore;
using VideoCharacter.Data;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Diagnostics;
using VideoCharacter.Exceptions;
using FluentValidation;
using FluentValidation.AspNetCore;
using VideoCharacter.Dtos;
using Microsoft.AspNetCore.Mvc;
using ApiBehaviorOptions = Microsoft.AspNetCore.Mvc.ApiBehaviorOptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.dotnet add package Scalar.AspNetCore
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=video.db"));

builder.Services.AddScoped<IVideoGameCharacterService, VideoGameCharacterService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .Select(x => new
            {
                field = x.Key,
                messages = x.Value.Errors.Select(e => e.ErrorMessage)
            });

        return new BadRequestObjectResult(new
        {
            error = "Validation failed",
            details = errors
        });
    };
});

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CharacterCreateDtoValidator>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("super_secret_key_123"))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();
app.MapControllers();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        context.Response.ContentType = "application/json";

        if (exception is ApiException ex)
        {
            context.Response.StatusCode = ex.StatusCode;

            await context.Response.WriteAsJsonAsync(new
            {
                error = ex.Message
            });
        }
        else
        {
            context.Response.StatusCode = 500;

            await context.Response.WriteAsJsonAsync(new
            {
                error = "Internal server error"
            });
        }
    });
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
