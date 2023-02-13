using Domain;
using Domain.Repositories;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace TodoApiDTO.Extensions
{
	public static class RepositoryExtensions
	{
		public static IServiceCollection RegisterRepositories(this IServiceCollection services)
		{
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			services.AddScoped<ITodoItemRepository, TodoItemRepository>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();

			return services;
		}
	}
}