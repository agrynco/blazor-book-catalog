namespace BookCatalog.API;

using BookCatalog.API.Entities;
using BookCatalog.API.Repositories;

public static class BooksEndpoints
{
	public static void MapBookEndpoints(this IEndpointRouteBuilder routes)
	{
		var booksGroup = routes.MapGroup("/api/books")
			.WithTags("Books");

		booksGroup.MapGet("/", (
			IBookRepository repo,
			string? title,
			string? author,
			string? genre,
			int page = 1,
			int pageSize = 10) =>
		{
			var filtered = repo.Search(title, author, genre).ToList();
			int totalCount = filtered.Count;

			var items = filtered
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToList();

			return Results.Ok(new
			{
				Page = page,
				PageSize = pageSize,
				TotalCount = totalCount,
				Items = items
			});
		});

		booksGroup.MapGet("/{id:int}", (IBookRepository repo, int id) =>
		{
			Book? book = repo.GetById(id);
			return book is not null ? Results.Ok(book) : Results.NotFound();
		});

		booksGroup.MapPost("/", (IBookRepository repo, Book newBook) =>
		{
			Book createdBook = repo.Add(newBook);
			return Results.Created($"/{createdBook.Id}", createdBook);
		});

		booksGroup.MapPut("/{id:int}", (IBookRepository repo, int id, Book updatedBook) =>
		{
			Book? existing = repo.Update(id, updatedBook);
			return existing is not null ? Results.Ok(existing) : Results.NotFound();
		});

		booksGroup.MapDelete("/{id:int}", (IBookRepository repo, int id) =>
		{
			bool deleted = repo.Delete(id);
			return deleted ? Results.NoContent() : Results.NotFound();
		});
	}
}