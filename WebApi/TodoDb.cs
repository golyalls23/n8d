using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {

    }

    public DbSet<TodoItem> TodoItems { get; set; }
}
