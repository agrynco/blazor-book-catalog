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

	IEnumerable<Book> Search(string? title, string? author, string? genre);
}