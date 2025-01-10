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
			string? sortBy = "title",
			string? sortOrder = "asc",
			int page = 1,
			int pageSize = 10) =>
		{
			var filtered = repo.Search(title, author, genre).ToList();

			filtered = sortBy?.ToLower() switch
			{
				"title" => sortOrder?.ToLower() == "desc"
					? filtered.OrderByDescending(b => b.Title).ToList()
					: filtered.OrderBy(b => b.Title).ToList(),
				"author" => sortOrder?.ToLower() == "desc"
					? filtered.OrderByDescending(b => b.Author).ToList()
					: filtered.OrderBy(b => b.Author).ToList(),
				"genre" => sortOrder?.ToLower() == "desc"
					? filtered.OrderByDescending(b => b.Genre).ToList()
					: filtered.OrderBy(b => b.Genre).ToList(),
				_ => filtered.OrderBy(b => b.Title).ToList()
			};

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