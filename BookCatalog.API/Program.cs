using BookCatalog.API;
using BookCatalog.API.Extensions;
using BookCatalog.API.Repositories;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomSwagger();

builder.Services.AddSingleton<IBookRepository, BookRepository>();

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policy =>
	{
		policy.AllowAnyOrigin()
			.AllowAnyHeader()
			.AllowAnyMethod();
	});
});

WebApplication app = builder.Build();

app.UseCors();

app.UseCustomSwagger();

app.MapGet("/", () => "Hello World!");

app.MapBookEndpoints();

app.Run();