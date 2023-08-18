using System.Configuration;
using Application;
using Application.Interfaces;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Exceptions;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithExceptionDetails()
    .Enrich.WithMachineName()
    .CreateBootstrapLogger();

Log.Information("Starting up...");

// Setting up logger
builder.Host.UseSerilog();

// Adding database context
var connectionString = builder.Configuration.GetConnectionString("TodoAppDB");
builder.Services.AddDbContext<TodoAppDbContext>(opt => opt.UseSqlServer(connectionString));

// Adding traditional controllers
builder.Services.AddControllers();

// Adding Swagger documentation
builder.Services.AddSwaggerGen();

// Adding inner layers
builder.Services
    .AddApplication()
    .AddInfrastructure();

// Adding repositories
builder.Services.AddScoped<ITodoItemsRepository, TodoItemsRepository>();

var app = builder.Build();
app.UseSerilogRequestLogging();

// Adding Global Exception Handler
app.ConfigureExceptionMiddleware();

// Adding Swagger only to Development Environment 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();