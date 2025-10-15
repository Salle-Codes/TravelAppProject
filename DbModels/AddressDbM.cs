using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using Seido.Utilities.SeedGenerator;
using Models;

namespace DbModels;
[Table("Addresses")]
sealed public class AddressDbM : Address, ISeed<AddressDbM>, IEquatable<AddressDbM>
{
    [Key]     
    public override Guid AddressId { get; set; }
    [Required]
    public override string StreetAddress { get; set; }
    [Required]
    public override string City { get; set; }
    [Required]
    public override string Country { get; set; }


    #region implementing IEquatable
    public bool Equals(AddressDbM other) => (other != null) && ((StreetAddress, City, Country) ==
        (other.StreetAddress, other.City, other.Country));

    public override bool Equals(object obj) => Equals(obj as AddressDbM);
    public override int GetHashCode() => (StreetAddress, City, Country).GetHashCode();
    #endregion

    #region correcting the Navigation properties migration error caused by using interfaces
    [NotMapped]
    public override List<IAttraction> Attractions { get => AttractionsDbM?.ToList<IAttraction>(); set => new NotImplementedException(); }

    [JsonIgnore]
    public List<AttractionDbM> AttractionsDbM { get; set; }
    #endregion

    #region randomly seed this instance
    public override AddressDbM Seed(SeedGenerator seedGenerator)
    {
        base.Seed(seedGenerator);
        return this;
    }
    #endregion

    #region constructors
    public AddressDbM() { }
    #endregion
}


