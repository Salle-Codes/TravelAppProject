using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Models;
using Seido.Utilities.SeedGenerator;

namespace DbModels;

[Table("Attractions")]
[Index(nameof(Name), nameof(Description), IsUnique = true)]
sealed public class AttractionDbM : Attraction,ISeed<AttractionDbM> , IEquatable<AttractionDbM>
{
    [Key]
    public override Guid AttractionId { get; set; }
    [Required]
    public override string Name { get; set; }
    [Required]
    public override string Description { get; set; }
    [Required]
    public override Category Category { get; set; }
    [Required]
    public Guid AddressId { get; set; }

    #region correcting the Navigation properties migration error caused by using interfaces
    [NotMapped]
    public override IAddress Address { get => AddressDbM; set => new NotImplementedException(); }

    [JsonIgnore]
    [ForeignKey("AddressId")]
    public AddressDbM AddressDbM { get; set; } = null;    //This is implemented in the database table
    #endregion

    #region implementing IEquatable
    public bool Equals(AttractionDbM other) => (other != null) && ((this.Name, this.Description) ==
        (other.Name, other.Description));
    public override bool Equals(object obj) => Equals(obj as AttractionDbM);
    public override int GetHashCode() => (Name, Description).GetHashCode();
    #endregion

    public override AttractionDbM Seed(SeedGenerator seeder)
    {
        base.Seed(seeder);
        return this;
    }

    #region constructors
    public AttractionDbM() { }
    #endregion
}