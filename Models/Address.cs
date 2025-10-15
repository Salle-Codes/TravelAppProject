using Seido.Utilities.SeedGenerator;

namespace Models;

public class Address : IAddress, ISeed<Address>, IEquatable<Address>
{
    public virtual Guid AddressId { get; set; }

    public virtual string StreetAddress { get; set; }
    public virtual string City { get; set; }
    public virtual string Country { get; set; }

    public override string ToString() => $"{StreetAddress}, {City}, {Country}";

    public virtual List<IAttraction> Attractions { get; set; } = null;

    #region constructors
    public Address() { }
    public Address(Address org)
    {
        this.Seeded = org.Seeded;

        this.AddressId = org.AddressId;
        this.StreetAddress = org.StreetAddress;
        this.City = org.City;
        this.Country = org.Country;
    }
    #endregion

    #region implementing IEquatable
    public bool Equals(Address other) => (other != null) && ((this.StreetAddress, this.City, this.Country) ==
        (other.StreetAddress, other.City, other.Country));

    public override bool Equals(object obj) => Equals(obj as Address);
    public override int GetHashCode() => (StreetAddress, City, Country).GetHashCode();
    #endregion

    #region Seeder
    public bool Seeded { get; set; } = false;
    public virtual Address Seed(SeedGenerator seeder)
    {
        Seeded = true;
        AddressId = Guid.NewGuid();
        Country = seeder.Country;
        StreetAddress = seeder.StreetAddress(Country);
        City = seeder.City(Country);
        return this;
    }
    #endregion
}


