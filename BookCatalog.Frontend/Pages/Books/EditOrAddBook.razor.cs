using Microsoft.AspNetCore.Components;

namespace BookCatalog.Frontend.Pages.Books;

using System.Net.Http.Json;
using BookCatalog.Frontend.Models;

public partial class EditOrAddBook
{
	[Parameter]
	public int? Id { get; set; }

	private AddOrEditBook _addOrEditBook = new AddOrEditBook();

	private string SubmitButtonText => _addOrEditBook.Id == 0 ? "Add Book" : "Save Changes";

	protected override async Task OnInitializedAsync()
	{
		if (Id.HasValue && Id.Value != 0)
		{
			var existingBook = await Http.GetFromJsonAsync<AddOrEditBook>($"api/books/{Id.Value}");
			if (existingBook != null)
			{
				_addOrEditBook = existingBook;
			}
		}
	}

	private async Task HandleValidSubmit()
	{
		if (_addOrEditBook.Id == 0)
		{
			await Http.PostAsJsonAsync("api/books", _addOrEditBook);
		}
		else
		{
			await Http.PutAsJsonAsync($"api/books/{_addOrEditBook.Id}", _addOrEditBook);
		}


		NavigationManager.NavigateTo("/books");
	}

	private void Cancel()
	{
		NavigationManager.NavigateTo("/books");
	}
}