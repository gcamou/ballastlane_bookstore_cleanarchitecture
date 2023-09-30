namespace Application.Books.Commands.UpdateBook
{
    public class UpdateBookResponse
    {
        public required Guid Id { get; set; }
        public required string Author { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
    }
}