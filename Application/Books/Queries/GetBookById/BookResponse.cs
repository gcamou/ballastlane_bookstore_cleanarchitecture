
namespace Application.Books.Queries.GetBookById;

public class BookResponse
{
    public Guid Id { get; set; }
    public required string Author { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
}