using Microsoft.EntityFrameworkCore;
using N8D.Entities;

namespace N8D.Repos;

public class TodoDbContext(DbContextOptions<TodoDbContext> options) : DbContext(options)
{
    public DbSet<TodoItem> TodoItems { get; set; }
}
