using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {

    }

    public DbSet<TodoItem> TodoItems { get; set; }
}
