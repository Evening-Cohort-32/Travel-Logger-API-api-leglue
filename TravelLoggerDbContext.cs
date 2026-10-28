using Microsoft.EntityFrameworkCore;

using Travel_Logger_API_api_leglue.Models;

public class TravelLoggerDbContext : DbContext
{

    public DbSet<City> Cities { get; set; }
    public DbSet<Log> Logs { get; set; }
    public DbSet<Recommendation> Recommendations { get; set; }
    public DbSet<Upvote> Upvotes { get; set; }
    public DbSet<User> Users { get; set; }

    public TravelLoggerDbContext(DbContextOptions<TravelLoggerDbContext> context) : base(context)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>().HasData(new City[]
        {
            new City {Id = 1, Name = "New Orleans"},
            new City {Id = 2, Name = "Nashville"},
            new City {Id = 3, Name = "Baltimore"},
            new City {Id = 4, Name = "Austin"}
        });
        modelBuilder.Entity<Log>().HasData(new Log[]
        {
            new Log {Id = 1, UserId = 1, CityId = 1, LogBody = "New Orleans is the best city that smells like pee that I've ever been to."},
            new Log {Id = 2, UserId = 2, CityId = 2, LogBody = "It was okay, but there weren't enough cowboys. Wouldn't recommend."},
            new Log {Id = 3, UserId = 3, CityId = 3, LogBody = "I came because of The Wire. I stayed for the robust carrier pigeon culture."},
            new Log {Id = 4, UserId = 1, CityId = 4, LogBody = "They got a dang subway here. Who would have guessed?"},
            new Log {Id = 5, UserId = 3, CityId = 1, LogBody = "Saw a golden man standing still in the street. Didn't fool me."}
        });
        modelBuilder.Entity<Recommendation>().HasData(new Recommendation[]
        {
            new Recommendation {Id = 1, Name = "French Quarter", UserId = 1, CityId = 1, RecommendationBody = "The place where you get to see the architecture that makes New Orleans, New Orleans. Be careful of the puddles, though."},
            new Recommendation {Id = 2, Name = "The Golden Man", UserId = 3, CityId = 1, RecommendationBody = "Don't let him fool you. He's not a statue, but he is golden and stands very still."},
            new Recommendation {Id = 3, Name = "Parking Lot", UserId = 2, CityId = 2, RecommendationBody = "They let you park a car here."},
            new Recommendation {Id = 4, Name = "Baltimore PD", UserId = 3, CityId = 3, RecommendationBody = "Just like in The Wire!"},
            new Recommendation {Id = 5, Name = "Subway Car", UserId = 1, CityId = 4, RecommendationBody = "They put a dang train in the ground."}
        });
        modelBuilder.Entity<User>().HasData(new User[]
        {
            new User {Id = 1, Username = "Mustard Mike", Email = "MichaelMustard@user.com", Description = "I'm a man of many mustards."},
            new User {Id = 2, Username = "Responsible Richard", Email = "Richard@Responsible.com", Description = "You can count on me to eat green peas."},
            new User {Id = 3, Username = "Rich Stanley", Email = "MoneyBags@chase.com", Description = "I'm a monied lad with lots of money bags."}
        });
        modelBuilder.Entity<Upvote>().HasData(new Upvote[]
        {
            new Upvote {Id = 1, UserId = 2, RecommendationId = 1},
            new Upvote {Id = 2, UserId = 1, RecommendationId = 2},
            new Upvote {Id = 3, UserId = 3, RecommendationId = 1},
            new Upvote {Id = 4, UserId = 3, RecommendationId = 5},
            new Upvote {Id = 5, UserId = 1, RecommendationId = 3}
        });

    }
}