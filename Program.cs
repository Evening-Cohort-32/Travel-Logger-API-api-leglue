using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Travel_Logger_API_api_leglue.Models;
using Travel_Logger_API_api_leglue.Models.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<TravelLoggerDbContext>(builder.Configuration["TravelLoggerDbConnectionString"]);

var app = builder.Build();

// Comment out HTTPS redirection for now to simplify testing
// app.UseHttpsRedirection();

// Configure Swagger for all environments during development
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

// Add all endpoints here
app.MapPost("/api/users", (TravelLoggerDbContext db, User user) =>
{
    if (db.Users.Any(u => u.Email == user.Email))
    {
        return Results.BadRequest("A user with that email already exists");
    }

    db.Users.Add(user);
    db.SaveChanges();
    return Results.Created($"/api/users/{user.Id}", user);
});

app.MapGet("/api/users/signin/{email}", (TravelLoggerDbContext db, string email) =>
{
    User user = db.Users.SingleOrDefault(u => u.Email == email);
    if (user == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(new UserDTO
    {
        Id = user.Id,
        Username = user.Username,
        Email = user.Email
    });
});

app.MapPut("/api/logs/{id}", (TravelLoggerDbContext db, int id, int userId, Log log) =>
{
    Log logToUpdate = db.Logs.SingleOrDefault(l => l.Id == id);
    if (logToUpdate == null)
    {
        return Results.NotFound();
    }
    if (logToUpdate.UserId != userId)
    {
        return Results.StatusCode(403);
    }

    logToUpdate.LogBody = log.LogBody;
    db.SaveChanges();
    return Results.NoContent();

});

app.MapDelete("/api/logs/{id}", (TravelLoggerDbContext db, int id, int userId) =>
{
    Log log = db.Logs.SingleOrDefault(l => l.Id == id);
    if (log == null)
    {
        return Results.NotFound();
    }
    if (log.UserId != userId)
    {
        return Results.StatusCode(403);
    }

    db.Logs.Remove(log);
    db.SaveChanges();
    return Results.NoContent();
});

app.MapPut("/api/users/{id}", (TravelLoggerDbContext db, int id, int userId, User user) =>
{
    if (id != userId)
    {
        return Results.StatusCode(403);
    }

    User userToUpdate = db.Users.SingleOrDefault(u => u.Id == id);
    if (userToUpdate == null)
    {
        return Results.NotFound();
    }

    userToUpdate.Description = user.Description;
    userToUpdate.ImageUrl = user.ImageUrl;
    db.SaveChanges();
    return Results.NoContent();
});

app.Run();