namespace Application.Books.Commands.CreateBook;
public class CreateBookRequest
{
    public required string Author { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
}