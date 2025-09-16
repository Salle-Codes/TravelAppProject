using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using Seido.Utilities.SeedGenerator;
using Models;

namespace DbModels;
[Table("Addresses")]
[Index(nameof(StreetAddress), nameof(City), nameof(Country), IsUnique = true)]
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
    [NotMapped] //removed from EFC 
    public override IAttraction Attraction { get => AttractionsDbM; set => new NotImplementedException(); }

    [JsonIgnore] //do not include in any json response from the WebApi
    public AttractionDbM AttractionsDbM { get; set; } = null;
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


