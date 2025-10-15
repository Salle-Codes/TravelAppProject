using System;

namespace Models;

public interface IAttraction
{
    public Guid AttractionId { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    public IAddress Address { get; set; }
    public Category Category { get; set; }
    public List<IComments> Comments { get; set; }
}