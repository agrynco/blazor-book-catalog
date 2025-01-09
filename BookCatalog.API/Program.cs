using BookCatalog.API;
using BookCatalog.API.Extensions;
using BookCatalog.API.Repositories;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddCustomSwagger();

builder.Services.AddSingleton<IBookRepository, BookRepository>();

WebApplication app = builder.Build();

app.UseCustomSwagger();

app.MapGet("/", () => "Hello World!");

app.MapBookEndpoints();

app.Run();