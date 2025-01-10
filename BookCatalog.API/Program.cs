using System.Threading.RateLimiting;
using BookCatalog.API;
using BookCatalog.API.Extensions;
using BookCatalog.API.Repositories;
using Microsoft.AspNetCore.SignalR;
using Serilog;
using Serilog.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithClientIp()
    .Enrich.WithProperty("Application", "BookCatalog.API") // Add a property to all logs
    .WriteTo.Seq("http://localhost:5341")
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddCustomSwagger();

builder.Services.AddSingleton<IBookRepository, BookRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("SignalRCorsPolicy", policy =>
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
                PermitLimit = 10, // Maximum 10 requests
                Window = TimeSpan.FromMinutes(1), // Per 1 minute
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 2 // Maximum 2 queued requests
            }));
});

builder.Services.AddSignalR();

var app = builder.Build();

app.UseCors("SignalRCorsPolicy");

// app.UseRateLimiter();

app.MapHub<BooksHub>("/hubs/books");

app.MapGet("/test", () => Results.Ok("Request succeeded!"))
   .RequireRateLimiting("General");

app.UseCustomSwagger();

app.MapGet("/", () => "Hello World!");

var hubContext = app.Services.GetRequiredService<IHubContext<BooksHub>>();

app.MapBookEndpoints(hubContext);

app.Run();
