using Microsoft.EntityFrameworkCore;
using WebApi;
using WebApi.Interfaces;
using WebApi.Repositories;
using WebApi.Services;

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
