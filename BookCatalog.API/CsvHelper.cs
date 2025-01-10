namespace BookCatalog.API;

using BookCatalog.API.Entities;

public static class CsvHelper
{
	public static List<Book> ParseBooksFromCsv(string csvContent)
	{
		var books = new List<Book>();
		var lines = csvContent.Split(["\r\n", "\n"], StringSplitOptions.None);

		foreach (var line in lines.Skip(1)) // Skip header row
		{
			if (string.IsNullOrWhiteSpace(line)) continue;

			var columns = line.Split(',');
			if (columns.Length < 3) continue; // Ensure at least Title, Author, Genre

			books.Add(new Book
			{
				Title = columns[0].Trim(),
				Author = columns[1].Trim(),
				Genre = columns[2].Trim()
			});
		}

		return books;
	}
}