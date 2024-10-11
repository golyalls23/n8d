using Microsoft.EntityFrameworkCore;
using N8D.Entities;
using N8D.Repos.Interfaces;

namespace N8D.Repos.Implementations;

public class TodoRepository(TodoDbContext context) : ITodoRepository
{
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
        await _context.TodoItems.AddAsync(todoItem);
        await _context.SaveChangesAsync();
        return todoItem;
    }

    public async Task<TodoItem?> Update(int id, TodoItem todoItem)
    {

        var todoItemToUpdate = await _context.TodoItems.FindAsync(id);
        if (todoItemToUpdate is null)
            return null;

        todoItemToUpdate.Description = todoItem.Description;
        todoItemToUpdate.IsComplete = todoItem.IsComplete;
        await _context.SaveChangesAsync();
        return todoItemToUpdate;
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

    public async Task<int> MaxId()
    {
        var lastTodoItem = await _context.TodoItems.LastOrDefaultAsync();
        int id = lastTodoItem?.Id + 1 ?? 1;
        return id;
    }

}
