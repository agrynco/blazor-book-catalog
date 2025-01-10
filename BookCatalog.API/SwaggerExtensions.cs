namespace BookCatalog.API.Extensions;

public static class SwaggerExtensions
{
	public static void AddCustomSwagger(this IServiceCollection services)
	{
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();
	}

	public static void UseCustomSwagger(this WebApplication app)
	{
		app.UseSwagger();
		app.UseSwaggerUI(options =>
		{
			options.SwaggerEndpoint("/swagger/v1/swagger.json", "BookCatalog API v1");
		});
	}
}