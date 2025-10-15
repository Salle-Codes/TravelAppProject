using Models;

namespace Services;

public interface IAttractionService
{
    public Task<List<IAttraction>> GetFilteredAttractionsAsync(bool seeded, bool flat, string filtered);
    public Task<List<IAttraction>> GetAttractionsWithoutCommentsAsync(bool flat);
    public Task<IAttraction> GetAttractionWithCommentsAsync(Guid id, bool flat);
}