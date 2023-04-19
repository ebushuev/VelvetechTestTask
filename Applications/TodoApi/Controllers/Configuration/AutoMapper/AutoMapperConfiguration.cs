using Microsoft.Extensions.DependencyInjection;
using TodoApi.Controllers.Configuration.AutoMapper.AutoMapperProfiles;

namespace TodoApi.Controllers.Configuration.AutoMapper
{
    public static class AutoMapperConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(TodoItemApiDataProfile));
            services.AddAutoMapper(typeof(TodoItemServiceDataProfile));
        }
    }
}