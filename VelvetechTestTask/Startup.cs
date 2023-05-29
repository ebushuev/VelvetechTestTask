using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MediatR;
using TodoApiDto.Application.Interfaces;
using TodoApiDto.Persistance;
using TodoApiDto.Application.Implementations.Command.CreateToDoItem;
using TodoApiDto.Application.Implementations.Queries.GetToItem;
using AutoMapper;
using TodoApiDto.Application.Common;
using TodoApiDto.Application.Implementations.Command.UpdateToDoItem;
using TodoApiDto.Application.Implementations.Command.DeleteTodoItem;
using TodoApiDto.Domain.Entities;
using TodoApiDto.Application.Implementations.Queries.GetItems;

namespace TodoApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            MapperConfiguration mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            
            services.AddMediatR(typeof(Startup).Assembly);

            services.AddEntityFrameworkSqlServer();
            services.AddDbContextPool<ITodoApiDtoDbContext, TodoApiDtoDbContext>(builder =>
            {
                builder.EnableDetailedErrors(true);

                builder.UseSqlServer(Configuration.GetConnectionString("DbConnectionString"), b =>
                {
                    b.MigrationsAssembly(typeof(TodoApiDtoDbContext).Assembly.FullName);
                    b.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                });
            }, int.TryParse(Configuration["DbContextPoolSize"], out var poolSize) ? poolSize : 128);

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddFile(Configuration["FilePath"]);
            });

            services.AddTransient<IRequestHandler<CreateTodoItemCommand, Guid>, CreateTodoItemCommandHandler>();
            services.AddTransient<IRequestHandler<GetToDoItemQuery, TodoItem>, GetToDoItemQueryHandler>();
            services.AddTransient<IRequestHandler<UpdateToDoItemCommand, Unit>, UpdateToDoItemCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteTodoItemCommand, Unit>, DeleteTodoItemCommandHandler>();
            services.AddTransient<IRequestHandler<GetToDoItemQuery, TodoItem>, GetToDoItemQueryHandler>();
            services.AddTransient<IRequestHandler<GetToDoItemsQuery, IEnumerable<TodoItem>>, GetToDoItemsQueryHandler>();

            services.AddSwaggerGen();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
