using System;
using System.Collections.Generic;
using Seido.Utilities.SeedGenerator;

namespace Models
{
    public class User : IUser, ISeed<User>, IEquatable<User>
    {
        public virtual Guid UserId { get; set; }
        public virtual string UserName { get; set; }
        public virtual List<IComments> Comments { get; set; } = null;

        public override string ToString() => $"{UserName}";

        #region Seeder
        public bool Seeded { get; set; } = false;
        public virtual User Seed(SeedGenerator seeder)
        {
            Seeded = true;
            UserId = Guid.NewGuid();
            UserName = seeder.FullName;
            return this;
        }
        #endregion

        #region implementing IEquatable
        public bool Equals(User other) =>
            other != null && UserId == other.UserId && UserName == other.UserName;

        public override bool Equals(object obj) => Equals(obj as User);

        public override int GetHashCode() => (UserId, UserName).GetHashCode();
        #endregion
    }
}