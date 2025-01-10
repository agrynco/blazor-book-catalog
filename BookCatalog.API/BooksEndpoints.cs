namespace BookCatalog.API;

using BookCatalog.API.Entities;
using BookCatalog.API.Repositories;
using Microsoft.AspNetCore.SignalR;

public static class BooksEndpoints
{
	public static void MapBookEndpoints(this IEndpointRouteBuilder routes, IHubContext<BooksHub> hubContext)
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
			(var items, int totalCount) = repo.Search(title, author, genre, sortBy, sortOrder, page, pageSize);

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

		booksGroup.MapPost("/", async (IBookRepository repo, Book newBook) =>
		{
			Book createdBook = repo.Add(newBook);
			await hubContext.Clients.All.SendAsync("BookAdded", createdBook);
			return Results.Created($"/{createdBook.Id}", createdBook);
		});

		booksGroup.MapPut("/{id:int}", async (IBookRepository repo, int id, Book updatedBook) =>
		{
			Book? existing = repo.Update(id, updatedBook);
			if (existing is not null)
			{
				await hubContext.Clients.All.SendAsync("BookUpdated", existing); // Notify clients
				return Results.Ok(existing);
			}
			return Results.NotFound();
		});

		booksGroup.MapDelete("/{id:int}", async (IBookRepository repo, int id) =>
		{
			bool deleted = repo.Delete(id);
			if (deleted)
			{
				await hubContext.Clients.All.SendAsync("BookDeleted", id); // Notify clients
				return Results.NoContent();
			}
			return Results.NotFound();
		});

		booksGroup.MapPost("/bulk-upload", async (IBookRepository repo, HttpRequest request) =>
		{
			if (!request.HasFormContentType)
			{
				return Results.BadRequest("Invalid content type. Please upload a valid CSV file.");
			}

			var form = await request.ReadFormAsync();
			var file = form.Files.FirstOrDefault();

			if (file is null || file.Length == 0)
			{
				return Results.BadRequest("No file uploaded or the file is empty.");
			}

			using var reader = new StreamReader(file.OpenReadStream());
			var csvContent = await reader.ReadToEndAsync();

			// Process CSV
			var books = CsvHelper.ParseBooksFromCsv(csvContent);

			foreach (Book book in books)
			{
				repo.Add(book);
			}

			return Results.Ok(new
			{
				Message = $"{books.Count} books imported successfully."
			});
		});
	}
}