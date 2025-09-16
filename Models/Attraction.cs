using Seido.Utilities.SeedGenerator;

namespace Models;
public enum Category { Museum,Park,HistoricalSite,AmusementPark,Zoo,Aquarium,Monument,ArtGallery,BotanicalGarden,Church,ShoppingDistrict,Theater,SportsVenue,CulturalCenter}
public class Attraction : IAttraction, ISeed<Attraction>
{
    public virtual Guid AttractionId { get; set; }

    public virtual string Name { get; set; }
    public virtual string Description { get; set; }
    public virtual IAddress Address { get; set; } = null;
    public virtual Category Category { get; set; }
    public override string ToString() => $"{Category}";

    #region Seeder
    public bool Seeded { get; set; } = false;

    public virtual Attraction Seed(SeedGenerator seeder)
    {
        Seeded = true;
        AttractionId = Guid.NewGuid();
        Category = seeder.FromEnum<Category>();
        Name = seeder.FirstName + "'s" + " " + Category.ToString();
        Description = $"A wonderful place to visit!";
        return this;
    }
    #endregion
}