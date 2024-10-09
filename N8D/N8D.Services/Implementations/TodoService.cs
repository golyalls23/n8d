using N8D.Models;
using N8D.Repos.Interfaces;
using N8D.Services.Interfaces;

namespace N8D.Services.Implementations;

public class TodoService(ITodoRepository todoRepository) : ITodoService
{
    private readonly ITodoRepository _todoRepository = todoRepository;

    public async Task<List<TodoItem>> GetAll()
    {
        var todoItems = new List<TodoItem>();
        var dbTodoItems = await _todoRepository.GetAll();
        foreach (var dbTodoItem in dbTodoItems)
        {
            todoItems.Add(new TodoItem()
            {
                Id = dbTodoItem.Id,
                Description = dbTodoItem.Description,
                IsComplete = dbTodoItem.IsComplete
            });
        }
        return todoItems;
    }

    public async Task<TodoItem?> GetById(int id)
    {
        var dbTodoItem = await _todoRepository.GetById(id);
        if (dbTodoItem is null)
            return null;

        return new TodoItem()
        {
            Id = dbTodoItem.Id,
            Description = dbTodoItem.Description,
            IsComplete = dbTodoItem.IsComplete
        };
    }

    public async Task<TodoItem?> Create(TodoItemCreate todoItemCreate)
    {
        var id = await _todoRepository.MaxId();
        var dbTodoItemNew = new Entities.TodoItem() { Id = id, Description = todoItemCreate.Description, IsComplete = todoItemCreate.IsComplete };

        var newDbTodoItem = await _todoRepository.Create(dbTodoItemNew);
        if (newDbTodoItem is null)
            return null;

        return new TodoItem()
        {
            Id = newDbTodoItem.Id,
            Description = newDbTodoItem.Description,
            IsComplete = newDbTodoItem.IsComplete
        };
    }

    public async Task<TodoItem?> Update(int id, TodoItemUpdate todoItem)
    {

        var dbTodoItem = new Entities.TodoItem()
        {
            Id = id,
            Description = todoItem.Description,
            IsComplete = todoItem.IsComplete,
        };
        var updatedTodoItem = await _todoRepository.Update(id, dbTodoItem);

        if (updatedTodoItem is null)
            return null;

        return new TodoItem()
        {
            Id = updatedTodoItem.Id,
            Description = updatedTodoItem.Description,
            IsComplete = updatedTodoItem.IsComplete
        };
    }

    public async Task<bool> Delete(int id)
    {
        return await _todoRepository.Delete(id);
    }
}
