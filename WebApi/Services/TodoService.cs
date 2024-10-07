using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Services;

public class TodoService(ILogger<TodoService> logger, TodoDbContext context) : ITodoService
{
    private readonly ILogger<TodoService> _logger = logger;
    private readonly TodoDbContext _context = context;

    public async Task<List<TodoItem>> GetAll()
    {
        return await _context.TodoItems.ToListAsync();
    }

    public async Task<TodoItem?> GetById(int id)
    {
        return await _context.TodoItems.FindAsync(id);
    }

    public async Task<TodoItem?> Create(TodoItem todoItem)
    {
        var lastTodoItem = await _context.TodoItems.LastOrDefaultAsync();
        var id = lastTodoItem?.Id + 1 ?? 1;
        var todoItemNew = new TodoItem() { Id = id, Description = todoItem.Description, IsComplete = todoItem.IsComplete };
        try
        {
            await _context.TodoItems.AddAsync(todoItemNew);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
        }
        return todoItemNew;

    }

    public async Task<TodoItem?> Update(int id, TodoItem todoItem)
    {

        var dbTodoItem = await _context.TodoItems.FindAsync(id);
        if (dbTodoItem is null)
            return null;

        dbTodoItem.Description = todoItem.Description;
        dbTodoItem.IsComplete = todoItem.IsComplete;
        await _context.SaveChangesAsync();

        return dbTodoItem;
    }

    public async Task<bool> Delete(int id)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);
        if (todoItem is null)
            return false;

        _context.TodoItems.Remove(todoItem);
        await _context.SaveChangesAsync();
        return true;
    }
}
