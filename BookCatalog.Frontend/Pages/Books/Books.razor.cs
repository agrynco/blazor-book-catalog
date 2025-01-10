namespace BookCatalog.Frontend.Pages.Books;

using System.Net.Http.Json;
using BookCatalog.Frontend.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

public partial class Books
{
	private string? _author;

	private List<Book>? _books;

	private int _currentPage = 1;
	private string? _genre;
	private int _pageSize = 5;

	private string? _sortBy = "title";
	private string? _sortOrder = "asc";

	private string? _title;
	private int _totalCount;
	private int _totalPages;
	private bool HasNext => _currentPage < _totalPages;

	private bool HasPrev => _currentPage > 1;

	[Inject]
	private HttpClient Http { get; set; } = null!;

	[Inject]
	private IJSRuntime JSRuntime { get; set; } = null!;

	[Inject]
	private NavigationManager NavigationManager { get; set; } = null!;

	protected override async Task OnInitializedAsync()
	{
		await LoadBooks();
	}

	private async Task LoadBooks()
	{
		var query = $"?title={_title}&author={_author}&genre={_genre}" +
		            $"&sortBy={_sortBy}&sortOrder={_sortOrder}" +
		            $"&page={_currentPage}&pageSize={_pageSize}";

		var result = await Http.GetFromJsonAsync<PagedResult<Book>>($"api/books{query}");

		if (result is not null)
		{
			_books = result.Items;
			_totalPages = (int)Math.Ceiling(result.TotalCount / (double)_pageSize);
		}
	}

	private async Task SearchBooks()
	{
		_currentPage = 1;
		await LoadBooks();
	}

	private async Task NextPage()
	{
		if (HasNext)
		{
			_currentPage++;
			await LoadBooks();
		}
	}

	private async Task PrevPage()
	{
		if (HasPrev)
		{
			_currentPage--;
			await LoadBooks();
		}
	}

	private void AddBook()
	{
		NavigationManager.NavigateTo("/books/edit-or-add");
	}

	private void EditBook(int id)
	{
		NavigationManager.NavigateTo($"/books/edit-or-add/{id}");
	}

	private async Task ApplySorting(string sortBy)
	{
		if (_sortBy == sortBy)
		{
			// Toggle sort order if the same column is clicked
			_sortOrder = _sortOrder == "asc" ? "desc" : "asc";
		}
		else
		{
			// Set new sort column and reset to ascending
			_sortBy = sortBy;
			_sortOrder = "asc";
		}

		// Reload books with new sorting
		await LoadBooks();
	}

	private string SortIndicator(string column)
	{
		if (_sortBy != column)
		{
			return string.Empty;
		}

		return _sortOrder == "asc" ? "⬆️" : "⬇️"; // Arrow indicators
	}

	private async Task DeleteBook(int id)
	{
		bool confirmed = await JSRuntime.InvokeAsync<bool>("confirm", new object[]
		{
			$"Are you sure you want to delete book ID {id}?"
		});

		if (!confirmed)
		{
			return;
		}

		await Http.DeleteAsync($"api/books/{id}");
		await LoadBooks();
	}
}