using Todo.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureRepositories();
builder.Services.ConfigureServices();

builder.Services.ConfigureSqlContext(builder.Configuration);

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.ConfigureSwagger();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsProduction())
    app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo v1");
});

app.MapControllers();

app.Run();