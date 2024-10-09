using Microsoft.EntityFrameworkCore;
using N8D.Repos;
using N8D.Repos.Implementations;
using N8D.Repos.Interfaces;
using N8D.Services;
using N8D.Services.Implementations;
using N8D.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// EntityFramework DbContext
builder.Services.AddDbContext<TodoDbContext>(opt =>
{
    opt.UseInMemoryDatabase("TodoList");
});

builder.Services
    .AddControllers(options =>
    {
        options.ReturnHttpNotAcceptable = true;
    })
    .AddNewtonsoftJson()
    .AddXmlSerializerFormatters();


builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddScoped<ITodoRepository, TodoRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
