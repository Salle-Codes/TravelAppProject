using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;

using Models;
using DbModels;
using DbContext;

namespace DbRepos;

public class AttractionDbRepos
{
    private ILogger<AttractionDbRepos> _logger;
    private readonly MainDbContext _dbContext;

    public AttractionDbRepos(ILogger<AttractionDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }

    public async Task<List<IAttraction>> GetFilteredAttractionsAsync(bool seeded, bool flat, string filter)
    {
        filter ??= "";
        IQueryable<AttractionDbM> query;

        if (flat)
        {
            query = _dbContext.Attractions.AsNoTracking();
        }
        else
        {
            query = _dbContext.Attractions.AsNoTracking()
                .Include(a => a.AddressDbM);
        }

        var filteredQuery = query
            .Where(a => a.Seeded == seeded &&
                (a.Name.ToLower().Contains(filter.ToLower()) ||
                 a.Description.ToLower().Contains(filter.ToLower()) ||
                 a.Category.ToString().ToLower().Contains(filter.ToLower()) ||
                 a.AddressDbM.City.ToLower().Contains(filter.ToLower()) ||
                 a.AddressDbM.Country.ToLower().Contains(filter.ToLower())));

        return await filteredQuery.ToListAsync<IAttraction>();
    }

    public async Task<List<IAttraction>> GetAttractionsWithoutCommentsAsync(bool flat)
    {
        IQueryable<AttractionDbM> query;

        if (flat)
        {
            query = _dbContext.Attractions.AsNoTracking();
        }
        else
        {
            query = _dbContext.Attractions
                .AsNoTracking()
                .Include(a => a.AddressDbM)
                .Include(a => a.CommentsDbM);
        }

        var attractions = await query
            .Where(a => !a.CommentsDbM.Any())
            .ToListAsync<IAttraction>();

        return attractions;
    }

    public async Task<IAttraction> GetAttractionWithCommentsAsync(Guid id, bool flat)
    {
        IQueryable<AttractionDbM> query;

        if (flat)
        {
            query = _dbContext.Attractions.AsNoTracking();
        }
        else
        {
            query = _dbContext.Attractions
                .AsNoTracking()
                .Include(a => a.AddressDbM)
                .Include(a => a.CommentsDbM)
                .ThenInclude(c => c.UserDbM);
        }

        var attraction = await query
            .FirstOrDefaultAsync(a => a.AttractionId == id);

        return attraction;
    }
}