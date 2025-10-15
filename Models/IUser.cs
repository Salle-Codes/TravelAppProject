using System;

namespace Models
{
    public interface IUser
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public List<IComments> Comments { get; set; }

    }
}