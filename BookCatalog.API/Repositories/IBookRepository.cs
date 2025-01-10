using BookCatalog.API.Entities;

namespace BookCatalog.API.Repositories;

public interface IBookRepository
{
	List<Book> GetAll();
	Book? GetById(int id);
	Book Add(Book book);
	Book? Update(int id, Book updatedBook);
	bool Delete(int id);
	int BulkAddFromCsv(Stream csvStream);

	(IEnumerable<Book> Items, int TotalCount) Search(
		string? title,
		string? author,
		string? genre,
		string? sortBy = "title",
		string? sortOrder = "asc",
		int page = 1,
		int pageSize = 10);
}