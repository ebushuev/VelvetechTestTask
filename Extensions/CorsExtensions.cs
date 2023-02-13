using Microsoft.Extensions.DependencyInjection;

namespace TodoApiDTO.Extensions
{
	public static class CorsExtensions
	{
		public static IServiceCollection RegisterCorsPolicy(this IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy("AllowAll",
					p => p.AllowAnyHeader()
						.AllowAnyOrigin()
						.AllowAnyMethod());
			});

			return services;
		}
	}
}