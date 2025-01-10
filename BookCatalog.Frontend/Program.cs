using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BookCatalog.Frontend;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiBaseAddress = Environment.GetEnvironmentVariable("API_BASE_ADDRESS") ?? "http://localhost:5000";

builder.Services.AddScoped(sp => new HttpClient
{
	BaseAddress = new Uri(apiBaseAddress)
});

await builder.Build().RunAsync();
