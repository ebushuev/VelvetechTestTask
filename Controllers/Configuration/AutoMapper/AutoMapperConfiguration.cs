using Microsoft.Extensions.DependencyInjection;
using TodoApiDTO.Controllers.Configuration.AutoMapper.AutoMapperProfiles;

namespace TodoApiDTO.Controllers.Configuration.AutoMapper
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