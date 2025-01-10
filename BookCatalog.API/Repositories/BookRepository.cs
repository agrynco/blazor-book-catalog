using BookCatalog.API.Entities;

namespace BookCatalog.API.Repositories;

public class BookRepository : IBookRepository
{
	private readonly List<Book> _books;
	private int _nextId;

	public BookRepository()
	{
		_books =
		[
			new Book
			{
				Id = 1,
				Title = "Clean Code",
				Author = "Robert C. Martin",
				Genre = "Software Development"
			},

			new Book
			{
				Id = 2,
				Title = "The Pragmatic Programmer",
				Author = "Andrew Hunt, David Thomas",
				Genre = "Software Development"
			},

			new Book
			{
				Id = 3,
				Title = "Design Patterns",
				Author = "Erich Gamma",
				Genre = "Software Engineering"
			},

			new Book
			{
				Id = 4,
				Title = "Refactoring",
				Author = "Martin Fowler",
				Genre = "Software Development"
			},

			new Book
			{
				Id = 5,
				Title = "Code Complete",
				Author = "Steve McConnell",
				Genre = "Software Development"
			},

			new Book
			{
				Id = 6,
				Title = "Head First Design Patterns",
				Author = "Eric Freeman",
				Genre = "Software Engineering"
			},

			new Book
			{
				Id = 7,
				Title = "Effective Java",
				Author = "Joshua Bloch",
				Genre = "Java Programming"
			},

			new Book
			{
				Id = 8,
				Title = "Introduction to Algorithms",
				Author = "Cormen, Leiserson, Rivest",
				Genre = "Computer Science"
			},

			new Book
			{
				Id = 9,
				Title = "Cracking the Coding Interview",
				Author = "Gayle Laakmann McDowell",
				Genre = "Interview Prep"
			},

			new Book
			{
				Id = 10,
				Title = "You Don't Know JS",
				Author = "Kyle Simpson",
				Genre = "JavaScript"
			},

			new Book
			{
				Id = 11,
				Title = "Clean Architecture",
				Author = "Robert C. Martin",
				Genre = "Software Architecture"
			},

			new Book
			{
				Id = 12,
				Title = "Working Effectively with Legacy Code",
				Author = "Michael Feathers",
				Genre = "Software Development"
			},

			new Book
			{
				Id = 13,
				Title = "Patterns of Enterprise Application Architecture",
				Author = "Martin Fowler",
				Genre = "Software Architecture"
			},

			new Book
			{
				Id = 14,
				Title = "Domain-Driven Design",
				Author = "Eric Evans",
				Genre = "Software Architecture"
			},

			new Book
			{
				Id = 15,
				Title = "The Mythical Man-Month",
				Author = "Fred Brooks",
				Genre = "Software Project Management"
			}
		];

		_nextId = _books.Max(b => b.Id) + 1;
	}

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
		Book? existingBook = GetById(id);

		if (existingBook is null)
		{
			return null;
		}

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

	public (IEnumerable<Book> Items, int TotalCount) Search(
		string? title,
		string? author,
		string? genre,
		string? sortBy = "title",
		string? sortOrder = "asc",
		int page = 1,
		int pageSize = 10)
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

		int totalCount = query.Count();

		query = sortBy?.ToLower() switch
		{
			"title" => sortOrder?.ToLower() == "desc"
				? query.OrderByDescending(b => b.Title)
				: query.OrderBy(b => b.Title),
			"author" => sortOrder?.ToLower() == "desc"
				? query.OrderByDescending(b => b.Author)
				: query.OrderBy(b => b.Author),
			"genre" => sortOrder?.ToLower() == "desc"
				? query.OrderByDescending(b => b.Genre)
				: query.OrderBy(b => b.Genre),
			_ => query.OrderBy(b => b.Title)
		};

		var paginatedItems = query
			.Skip((page - 1) * pageSize)
			.Take(pageSize)
			.ToList();

		return (paginatedItems, totalCount);
	}

}