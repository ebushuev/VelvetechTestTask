using Application.Services;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace TodoApiDTO.Extensions
{
	public static class ServicesExtensions
	{
		public static IServiceCollection RegisterServices(this IServiceCollection services)
		{
			services.AddScoped<ITodoItemService, TodoItemService>();

			return services;
		}
	}
}