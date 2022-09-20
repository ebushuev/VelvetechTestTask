using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Mapping;

namespace TodoApi.Extensions {
	public static class MapperConfigurator {
		public static IServiceCollection AddAutoMapper(this IServiceCollection services) {
			var mapperConfig = new MapperConfiguration(c => { 
				c.AddProfile<TodoItemProfile>();
			});

			services.AddSingleton(s => mapperConfig.CreateMapper());

			return services;
		}
	}
}
