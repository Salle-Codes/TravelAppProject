using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;

using Seido.Utilities.SeedGenerator;
using DbModels;
using DbContext;
using Configuration;

namespace DbRepos;

public class AdminDbRepos
{
    private const string _seedSource = "./app-seeds.json";
    private readonly ILogger<AdminDbRepos> _logger;
    private Encryptions _encryptions;
    private readonly MainDbContext _dbContext;

    public async Task SeedAsync()
    {
        _dbContext.Attractions.RemoveRange(_dbContext.Attractions);
        _dbContext.Addresses.RemoveRange(_dbContext.Addresses);
        _dbContext.CreditCards.RemoveRange(_dbContext.CreditCards);
        _dbContext.Users.RemoveRange(_dbContext.Users);
        _dbContext.Comments.RemoveRange(_dbContext.Comments);
        //Create a seeder
        var fn = Path.GetFullPath(_seedSource);
        var seeder = new SeedGenerator(fn);

        var creditcards = seeder.ItemsToList<CreditCardDbM>(1000);
        var addresses = seeder.ItemsToList<AddressDbM>(100)
            .GroupBy(a => new { a.StreetAddress, a.City, a.Country })
            .Select(g => g.First())
            .ToList();

        _dbContext.Addresses.AddRange(addresses);
        await _dbContext.SaveChangesAsync(); // Save addresses first

        var addressIds = _dbContext.Addresses.Select(a => a.AddressId).ToList();

        var attractions = seeder.ItemsToList<AttractionDbM>(500)
            .GroupBy(a => new { a.Name, a.Description })
            .Select(g => g.First())
            .ToList();

        // Assign only valid AddressIds to attractions
        foreach (var attraction in attractions)
        {
            attraction.AddressId = addressIds[seeder.Next(0, addressIds.Count)];
        }

        _dbContext.Attractions.AddRange(attractions);
        await _dbContext.SaveChangesAsync(); // Save attractions next

        var users = seeder.ItemsToList<UserDbM>(200)
            .GroupBy(u => u.UserName)
            .Select(g => g.First())
            .ToList();

        _dbContext.Users.AddRange(users);
        await _dbContext.SaveChangesAsync(); // Save users next

        var userIds = _dbContext.Users.Select(u => u.UserId).ToList();
        var attractionIds = _dbContext.Attractions.Select(a => a.AttractionId).ToList();

        var comments = seeder.ItemsToList<CommentsDbM>(seeder.Next(0, 20))
            .GroupBy(c => new { c.Content, c.Type })
            .Select(g => g.First())
            .ToList();

        foreach (var comment in comments)
        {
            comment.UserId = userIds[seeder.Next(0, userIds.Count)];
            comment.AttractionId = attractionIds[seeder.Next(0, attractionIds.Count)];
        }
        _dbContext.Comments.AddRange(comments);
        await _dbContext.SaveChangesAsync(); // Save comments last
    }

    public AdminDbRepos(ILogger<AdminDbRepos> logger, Encryptions encryptions, MainDbContext context)
    {
        _logger = logger;
        _encryptions = encryptions;
        _dbContext = context;
    }
}
