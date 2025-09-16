using System;
using Seido.Utilities.SeedGenerator;

public enum CommentType { Amazing, Fantastic, Horrible, Dreadful, Spectacular, Mediocre, Average, Poor, Terrible, Unforgettable }
namespace Models
{
    public interface IComments
    {
        public Guid CommentId { get; set; } 
        public string Content { get; set; }
        public Guid AttractionId { get; set; }
        public Guid UserId { get; set; }
    }
}