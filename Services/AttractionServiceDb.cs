using Microsoft.Extensions.Logging;

using Models;
using DbRepos;

namespace Services;

public class AttractionServiceDb : IAttractionService
{
    private readonly AttractionDbRepos _repo = null;
    private readonly ILogger<AttractionServiceDb> _logger = null;


    public AttractionServiceDb(AttractionDbRepos repo)
    {
        _repo = repo;
    }
    public AttractionServiceDb(AttractionDbRepos repo, ILogger<AttractionServiceDb> logger) : this(repo)
    {
        _logger = logger;
    }

    public Task<List<IAttraction>> GetFilteredAttractionsAsync(bool seeded, bool flat, string filtered) => _repo.GetFilteredAttractionsAsync(seeded, flat, filtered);
    public Task<List<IAttraction>> GetAttractionsWithoutCommentsAsync(bool flat) => _repo.GetAttractionsWithoutCommentsAsync(flat);
    public Task<IAttraction> GetAttractionWithCommentsAsync(Guid id, bool flat) => _repo.GetAttractionWithCommentsAsync(id, flat);
}