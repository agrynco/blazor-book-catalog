namespace BookCatalog.API;

using Microsoft.AspNetCore.SignalR;

public class BooksHub : Hub
{
	public async Task NotifyBookListUpdated()
	{
		await Clients.All.SendAsync("BookListUpdated");
	}
}
