using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Models;
using Seido.Utilities.SeedGenerator;
using System.Collections.Generic;

namespace DbModels;

[Table("Users")]
[Index(nameof(UserName))]
sealed public class UserDbM : User, ISeed<UserDbM>, IEquatable<UserDbM>
{
    [Key]
    public override Guid UserId { get; set; }
    [Required]
    public override string UserName { get; set; }

    #region correcting the Navigation properties migration error caused by using interfaces
    [NotMapped]
    public override List<IComments> Comments { get => CommentsDbM?.ToList<IComments>(); set => new NotImplementedException(); }

    [JsonIgnore]
    public List<CommentsDbM> CommentsDbM { get; set; } = null;
    #endregion

    #region implementing IEquatable
    public bool Equals(UserDbM other) =>
        other != null && UserId == other.UserId && UserName == other.UserName;

    public override bool Equals(object obj) => Equals(obj as UserDbM);

    public override int GetHashCode() => (UserId, UserName).GetHashCode();
    #endregion

    public override UserDbM Seed(SeedGenerator seeder)
    {
        base.Seed(seeder);
        return this;
    }

    #region constructors
    public UserDbM() { }
    #endregion
}