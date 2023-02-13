using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace TodoApiDTO.Extensions
{
	public static class SwaggerExtensions
	{
		public static IServiceCollection AddSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "TODO API",
					Version = "v1",
					Description = "TODO CRUD API",
					Contact = new OpenApiContact
					{
						Name = "Nodar Aronia",
						Email = "nodararonia@gmail.com"
					},
				});
			});

			return services;
		}
	}
}