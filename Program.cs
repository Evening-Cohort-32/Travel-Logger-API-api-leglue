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

// User Endpoints
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

app.MapGet("/api/users/{id}", (TravelLoggerDbContext db, int id) =>
{
    return db.Users
    .Include(u => u.Recommendations)
    .Include(u => u.Logs)
    .Select(u => new UserDTO
    {
        Id = u.Id,
        Username = u.Username,
        Email = u.Email,
        Description = u.Description,
        ImageUrl = u.ImageUrl,
        Recommendations = u.Recommendations.Select(r => new RecommendationDTO
        {
            Id = r.Id,
            Name = r.Name,
            UserId = r.UserId,
            CityId = r.CityId,
            RecommendationBody = r.RecommendationBody
        }).ToList(),
        Logs = u.Logs.Select(l => new LogDTO
        {
            Id = l.Id,
            UserId = l.UserId,
            CityId = l.CityId,
            LogBody = l.LogBody
        }).ToList()
    })
    .Single(u => u.Id == id);
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

app.MapGet("/api/cities/{cityId}/users", (TravelLoggerDbContext db, int cityId) =>
{
    // List users by city (based on their most recent log)
});

// City Endpoints
// Users, Logs, Recommendations
app.MapGet("/api/cities", (TravelLoggerDbContext db) =>
{
    return db.Cities
    .Include(c => c.Users)
    .Include(c => c.Logs)
    .Include(c => c.Recommendations)
    .Select(c => new CityDTO
    {
        Id = c.Id,
        Name = c.Name,
        Users = c.Users.Select(u => new UserDTO
        {
            Id = u.Id,
            Username = u.Username,
            Email = u.Email,
            Description = u.Description,
            ImageUrl = u.ImageUrl
        }).ToList(),
        Logs = c.Logs.Select(l => new LogDTO
        {
            Id = l.Id,
            UserId = l.UserId,
            CityId = l.CityId,
            LogBody = l.LogBody
        }).ToList(),
        Recommendations = c.Recommendations.Select(r => new RecommendationDTO
        {
            Id = r.Id,
            Name = r.Name,
            UserId = r.UserId,
            CityId = r.CityId,
            RecommendationBody = r.RecommendationBody
        }).ToList()

    }).ToList();
});

app.MapGet("/api/cities/{id}", (TravelLoggerDbContext db, int id) =>
{
    return db.Cities
    .Include(c => c.Users)
    .Include(c => c.Logs)
    .Include(c => c.Recommendations)
    .Select(c => new CityDTO
    {
        Id = c.Id,
        Name = c.Name,
        Users = c.Users.Select(u => new UserDTO
        {
            Id = u.Id,
            Username = u.Username,
            Email = u.Email,
            Description = u.Description,
            ImageUrl = u.ImageUrl
        }).ToList(),
        Logs = c.Logs.Select(l => new LogDTO
        {
            Id = l.Id,
            UserId = l.UserId,
            CityId = l.CityId,
            LogBody = l.LogBody
        }).ToList(),
        Recommendations = c.Recommendations.Select(r => new RecommendationDTO
        {
            Id = r.Id,
            Name = r.Name,
            UserId = r.UserId,
            CityId = r.CityId,
            RecommendationBody = r.RecommendationBody
        }).ToList()

    }).Single(c => c.Id == id);

});

// Log Endpoints


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



// User endpoints

app.Run();