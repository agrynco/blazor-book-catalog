namespace BookCatalog.API;

using BookCatalog.API.Entities;

public static class CsvHelper
{
	public static List<Book> ParseBooksFromCsv(string csvContent)
	{
		string[] lines = csvContent.Split(["\r\n", "\n"], StringSplitOptions.None);

		return (from line in lines.Skip(1)
			where !string.IsNullOrWhiteSpace(line)
			select line.Split(',') into columns
			where columns.Length >= 3
			select new Book
			{
				Title = columns[0].Trim(),
				Author = columns[1].Trim(),
				Genre = columns[2].Trim()
			}).ToList();
	}
}