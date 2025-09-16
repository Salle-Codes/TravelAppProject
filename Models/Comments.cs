using System;
using System.ComponentModel.DataAnnotations.Schema;
using Seido.Utilities.SeedGenerator;

namespace Models
{
    public class Comments : IComments, ISeed<Comments>, IEquatable<Comments>
    {
        public virtual CommentType Type { get; set; }

        public virtual Guid CommentId { get; set; }
        public virtual string Content { get; set; }
        public virtual Guid AttractionId { get; set; }
        public virtual Guid UserId { get; set; }

        [NotMapped]
        public virtual IUser User { get; set; } = null;
        [NotMapped]
        public virtual IAttraction Attraction { get; set; } = null;

        public override string ToString() => $"{Content}";

        #region Seeder
        public bool Seeded { get; set; } = false;
        public virtual Comments Seed(SeedGenerator seeder)
        {
            Seeded = true;
            CommentId = Guid.NewGuid();
            AttractionId = Guid.Empty;
            UserId = Guid.Empty;
            Type = seeder.FromEnum<CommentType>();
            Content = $"{Type} experience at this attraction!";
            return this;
        }
        #endregion

        #region implementing IEquatable
        public bool Equals(Comments other) => (other != null) && ((this.Content, this.Type) ==
            (other.Content, other.Type));
        public override bool Equals(object obj) => Equals(obj as Comments);
        public override int GetHashCode() => (Content, Type).GetHashCode();
        #endregion
    }
}