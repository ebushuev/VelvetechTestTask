using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
namespace TodoApi.Extensions {
	public static class SwaggerConfigurator {
		public static IServiceCollection AddVTSwagger(this IServiceCollection services) {
			services.AddSwaggerGen(c => {
				c.SwaggerDoc("v1", new OpenApiInfo {
					Title = "VelveTech API",
					Description = "API",
					Version = "v1"
				});
				c.EnableAnnotations();
			});
			return services;
		}

		public static IApplicationBuilder UseVTSwagger(this IApplicationBuilder app) {
			app.UseSwagger();
			app.UseSwaggerUI(c => {
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "VelveTech API v1");
				c.RoutePrefix = "swagger";
			});
			return app;
		}
	}
}
