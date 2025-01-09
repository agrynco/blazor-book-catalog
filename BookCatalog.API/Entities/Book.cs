namespace BookCatalog.API.Entities;

public class Book
{
	public int Id { get; set; }
	public required string Title { get; set; }
	public required string Author { get; set; }
	public required string Genre { get; set; }
}