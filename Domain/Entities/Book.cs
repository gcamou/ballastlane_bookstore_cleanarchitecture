using Domain.Primitives;

namespace Domain.Entities;

public class Book : Entity
{
    public required string Author{ get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
}