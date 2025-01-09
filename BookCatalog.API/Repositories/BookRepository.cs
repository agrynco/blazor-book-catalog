using BookCatalog.API.Entities;

namespace BookCatalog.API.Repositories;

public class BookRepository : IBookRepository
{
	private readonly List<Book> _books = new();
	private int _nextId = 1;

	public List<Book> GetAll() => _books;

	public Book? GetById(int id) => _books.FirstOrDefault(b => b.Id == id);

	public Book Add(Book book)
	{
		book.Id = _nextId++;
		_books.Add(book);
		return book;
	}

	public Book? Update(int id, Book updatedBook)
	{
		var existingBook = GetById(id);
		if (existingBook is null)
			return null;

		existingBook.Title = updatedBook.Title;
		existingBook.Author = updatedBook.Author;
		existingBook.Genre = updatedBook.Genre;

		return existingBook;
	}

	public bool Delete(int id)
	{
		var book = GetById(id);
		return book is not null && _books.Remove(book);
	}

	public int BulkAddFromCsv(Stream csvStream)
	{
		return 0;
	}

	public IEnumerable<Book> Search(string? title, string? author, string? genre)
	{
		var query = _books.AsQueryable();

		if (!string.IsNullOrWhiteSpace(title))
		{
			query = query.Where(b =>
				b.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
		}

		if (!string.IsNullOrWhiteSpace(author))
		{
			query = query.Where(b =>
				b.Author.Contains(author, StringComparison.OrdinalIgnoreCase));
		}

		if (!string.IsNullOrWhiteSpace(genre))
		{
			query = query.Where(b =>
				b.Genre.Contains(genre, StringComparison.OrdinalIgnoreCase));
		}

		return query.ToList();
	}
}