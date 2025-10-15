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
        _dbContext.Users.RemoveRange(_dbContext.Users);
        _dbContext.Comments.RemoveRange(_dbContext.Comments);
        //Create a seeder
        var fn = Path.GetFullPath(_seedSource);
        var seeder = new SeedGenerator(fn);

        var users = seeder.ItemsToList<UserDbM>(200);

        _dbContext.Users.AddRange(users);
        await _dbContext.SaveChangesAsync(); // Save users first
        var addresses = seeder.ItemsToList<AddressDbM>(100);

        _dbContext.Addresses.AddRange(addresses);
        await _dbContext.SaveChangesAsync(); // Save addresses next

        var addressIds = _dbContext.Addresses.Select(a => a.AddressId).ToList();

        var attractions = seeder.ItemsToList<AttractionDbM>(1000);

        // Assign only valid AddressIds to attractions
        foreach (var attraction in attractions)
        {
            attraction.AddressId = addressIds[seeder.Next(addressIds.Count)];
        }

        _dbContext.Attractions.AddRange(attractions);
        await _dbContext.SaveChangesAsync(); // Save attractions next



        var userIds = _dbContext.Users.Select(u => u.UserId).ToList();
        var attractionIds = _dbContext.Attractions.Select(a => a.AttractionId).ToList();

        var comments = new List<CommentsDbM>();

        foreach (var attractionId in attractionIds)
        {
            int numComments = seeder.Next(0, 21);
            for (int i = 0; i < numComments; i++)
            {
                var comment = seeder.ItemsToList<CommentsDbM>(1).First();
                comment.AttractionId = attractionId;
                comment.UserId = userIds[seeder.Next(0, userIds.Count)];
                comments.Add(comment);
            }
        }

        _dbContext.Comments.AddRange(comments);
        await _dbContext.SaveChangesAsync(); // Save comments last
    }

    public async Task RemoveSeedAsync()
    {
        _dbContext.Comments.RemoveRange(_dbContext.Comments);
        _dbContext.Attractions.RemoveRange(_dbContext.Attractions);
        _dbContext.Users.RemoveRange(_dbContext.Users);
        _dbContext.Addresses.RemoveRange(_dbContext.Addresses);
        await _dbContext.SaveChangesAsync();
    }

    public AdminDbRepos(ILogger<AdminDbRepos> logger, Encryptions encryptions, MainDbContext context)
    {
        _logger = logger;
        _encryptions = encryptions;
        _dbContext = context;
    }
}
