using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Models;
using Seido.Utilities.SeedGenerator;

namespace DbModels;

[Table("Comments")]
sealed public class CommentsDbM : Comments, ISeed<CommentsDbM>, IEquatable<CommentsDbM>
{
    [Key]
    public override Guid CommentId { get; set; }
    public override string Content { get; set; }
    [Required]
    public override CommentType Type { get; set; }
    public override Guid AttractionId { get; set; }
    public override Guid UserId { get; set; }

    #region correcting the Navigation properties migration error caused by using interfaces
    [NotMapped]
    public override IUser User { get => UserDbM; set => throw new NotImplementedException(); }

    [JsonIgnore]
    [ForeignKey("UserId")]
    public UserDbM UserDbM { get; set; } = null;

    [NotMapped]
    public override IAttraction Attraction { get => AttractionDbM; set => throw new NotImplementedException(); }

    [JsonIgnore]
    [ForeignKey("AttractionId")]
    public AttractionDbM AttractionDbM { get; set; } = null;
    #endregion

    #region implementing IEquatable
    public bool Equals(CommentsDbM other) =>
        other != null && (this.Content, this.Type) == (other.Content, other.Type);

    public override bool Equals(object obj) => Equals(obj as CommentsDbM);

    public override int GetHashCode() => (Content, Type).GetHashCode();
    #endregion

    public override CommentsDbM Seed(SeedGenerator seeder)
    {
        base.Seed(seeder);
        return this;
    }

    #region constructors
    public CommentsDbM() { }
    #endregion
}