using BookCatalog.Frontend;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

string apiBaseAddress = Environment.GetEnvironmentVariable("API_BASE_ADDRESS") ?? "http://localhost:5000";

builder.Services.AddScoped(_ => new HttpClient
{
	BaseAddress = new Uri(apiBaseAddress)
});

builder.Services.AddScoped(_ => new HubConnectionBuilder()
	.WithUrl($"{apiBaseAddress}/hubs/books")
	.WithAutomaticReconnect()
	.Build());

builder.Logging.SetMinimumLevel(LogLevel.Debug);
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

await builder.Build().RunAsync();