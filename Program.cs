using System;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Serilog;
using Serilog.OpenTelemetry;
using TodoApiDTO.Extensions;
using TodoApiDTO.Extensions.SerilogEnricher;
using TodoApiDTO.Models;
using TodoApiDTO.Service;
using TodoApiDTO.Service.DatabaseWrappers;
using TodoApiDTO.ServiceInterfaces;
using TodoApiDTO.ServiceInterfaces.DatabaseWrappers;
using TodoApiDTO.Validations;


var builder = WebApplication.CreateBuilder(args);

builder.AddCustomDb();

builder.Services.AddCustomSwagger();

builder.Host.UseSerilog((context, lc) => lc
    .Enrich.WithCaller()
    .Enrich.WithResource(
        ("server", Environment.MachineName),
        ("app", AppDomain.CurrentDomain.FriendlyName))
    .WriteTo.Console()
    .ReadFrom.Configuration(context.Configuration)
);

builder.Services.AddScoped<IDataBase, Postgres>();
builder.Services.AddScoped<ITodoService, TodoService>();

builder.Services.AddScoped<IValidator<TodoItemCreateModel>, TodoItemCreateValidator>();
builder.Services.AddScoped<IValidator<TodoItemUpdateModel>, TodoItemUpdateValidator>();

builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.AddCustomSwaggerUI();
}

Log.ForContext("Mode", app.Environment.EnvironmentName);
Log.Debug("App activated in [{Environment}] mode", app.Environment.EnvironmentName);


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.AddMigration();

app.Run();