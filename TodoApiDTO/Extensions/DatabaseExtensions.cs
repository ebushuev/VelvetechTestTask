using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TodoApiDTO.Extensions
{
	public static class DatabaseExtensions
	{
		public static IServiceCollection RegisterSQLDatabase(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<TodoContext>(options =>
				options.UseSqlServer(configuration["ConnectionStrings:DbContext"], sqlOptions => sqlOptions.EnableRetryOnFailure()));

			return services;
		}
	}
}