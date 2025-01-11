using System.Net.Http.Json;
using BookCatalog.Frontend.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;

namespace BookCatalog.Frontend.Pages.Books;

public partial class Books
{
    private string? _author;
    private List<Book>? _books;
    private int _currentPage = 1;
    private string? _genre;
    private readonly int _pageSize = 5;
    private string? _sortBy = "title";
    private string? _sortOrder = "asc";
    private string? _title;
    private int _totalPages;
    private IBrowserFile? _file;
    private bool HasNext => _currentPage < _totalPages;
    private bool HasPrev => _currentPage > 1;

    [Inject]
    private HttpClient Http { get; set; } = null!;

    [Inject]
    private IJSRuntime JsRuntime { get; set; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private HubConnection HubConnection { get; set; } = null!;

    [Inject]
    private ILogger<Books> Logger { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        Logger.LogInformation("Books component is initializing...");
        try
        {
            await LoadBooks();
            Logger.LogInformation("Books loaded successfully.");

            await InitializeSignalR();
            Logger.LogInformation("SignalR initialized successfully.");
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error initializing Books component");
        }
    }

    private async Task InitializeSignalR()
    {
        HubConnection.On<Book>("BookAdded", book =>
        {
            _books?.Add(book);
            StateHasChanged();
        });

        HubConnection.On<int>("BookDeleted", id =>
        {
            Book? book = _books?.SingleOrDefault(b => b.Id == id);

            if (book == null)
            {
                return;
            }

            _books!.Remove(book);
            StateHasChanged();
        });

        HubConnection.On<Book>("BookUpdated", updatedBook =>
        {
            var existingBook = _books?.SingleOrDefault(b => b.Id == updatedBook.Id);
            if (existingBook != null)
            {
                // Update the properties of the existing book
                existingBook.Title = updatedBook.Title;
                existingBook.Author = updatedBook.Author;
                existingBook.Genre = updatedBook.Genre;
                // Add any other properties you need to update
            }
            else
            {
                // If the book doesn't exist in the collection, optionally add it
                _books?.Add(updatedBook);
            }
            StateHasChanged();
        });

        // Start the connection
        try
        {
            await HubConnection.StartAsync();
            Console.WriteLine("SignalR connected.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error connecting to SignalR: {ex.Message}");
        }
    }

    private async Task LoadBooks()
    {

        try
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
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading books: {ex.Message}");
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
            _sortOrder = _sortOrder == "asc" ? "desc" : "asc";
        }
        else
        {
            _sortBy = sortBy;
            _sortOrder = "asc";
        }

        await LoadBooks();
    }

    private string SortIndicator(string column)
    {
        if (_sortBy != column)
        {
            return string.Empty;
        }

        return _sortOrder == "asc" ? "⬆️" : "⬇️";
    }

    private async Task DeleteBook(int id)
    {
        bool confirmed = await JsRuntime.InvokeAsync<bool>("confirm", new object[]
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

    private async Task UploadCsv()
    {
        if (_file is null)
        {
            return;
        }

        try
        {
            using var content = new MultipartFormDataContent();
            var fileContent = new StreamContent(_file.OpenReadStream());
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/csv");
            content.Add(fileContent, "file", _file.Name);

            var response = await Http.PostAsync("api/books/bulk-upload", content);

            if (response.IsSuccessStatusCode)
            {
                await LoadBooks();
                _file = null;
                StateHasChanged();
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Upload failed: {errorMessage}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error uploading file: {ex.Message}");
        }
    }

    private void HandleFileSelected(InputFileChangeEventArgs e)
    {
        if (e.FileCount <= 0)
        {
            return;
        }

        _file = e.File;
        Console.WriteLine($"Selected file: {_file.Name}");
    }
}
