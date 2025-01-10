using System.Threading.RateLimiting;
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

builder.Services.AddRateLimiter(options =>
{
	options.AddPolicy("General", context =>
		RateLimitPartition.GetFixedWindowLimiter(
			partitionKey: context.Connection.RemoteIpAddress?.ToString() ?? "global",
			factory: _ => new FixedWindowRateLimiterOptions
			{
				PermitLimit = 10,
				Window = TimeSpan.FromMinutes(1), // Per 1 minute
				QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
				QueueLimit = 2
			}));
});

WebApplication app = builder.Build();

app.UseRateLimiter();

app.MapGet("/test", () => Results.Ok("Request succeeded!")).RequireRateLimiting("General");

app.UseCors();

app.UseCustomSwagger();

app.MapGet("/", () => "Hello World!");

app.MapBookEndpoints();

app.Run();