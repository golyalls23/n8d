using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using N8D.Models;
using N8D.Services.Interfaces;

namespace N8D.WebApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class TodoController(ILogger<TodoController> logger, ITodoService todoService) : ControllerBase
{
    private readonly ILogger<TodoController> _logger = logger;
    private readonly ITodoService _todoService = todoService;

    //private List<TodoItem> TodoItems =
    //[
    //    new TodoItem() { Id = 1, Description = "A", IsComplete = false },
    //    new TodoItem() { Id = 2, Description = "B", IsComplete = true },
    //    new TodoItem() { Id = 3, Description = "C", IsComplete = false },
    //];

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetAll()
    {
        _logger.LogInformation("Todo GetAll Requested");
        var todoItems = await _todoService.GetAll();
        return Ok(todoItems);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<TodoItem>> GetById(int id)
    {
        _logger.LogInformation("Todo GetById: {id}", id);
        var todoItem = await _todoService.GetById(id);
        if (todoItem is null)
            return NotFound();

        return Ok(todoItem);
    }

    [HttpPost]
    public async Task<ActionResult<TodoItem>> Create(TodoItemCreate todoItemCreate)
    {
        var newTodo = await _todoService.Create(todoItemCreate);
        if (newTodo is null)
            return BadRequest();

        return CreatedAtAction(nameof(GetById), new { newTodo.Id }, newTodo);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<TodoItem>> UpdateTodo(int id, TodoItemUpdate todoItem)
    {
        if (id != todoItem.Id)
            return BadRequest();

        var dbTodoItem = await _todoService.GetById(id);
        if (dbTodoItem is null)
            return NotFound();

        var updatedTodo = await _todoService.Update(id, todoItem);
        return Ok(updatedTodo);

    }

    [HttpPatch]
    [Route("{id}")]
    public async Task<ActionResult> UpdateParitialTodo(int id, [FromBody] JsonPatchDocument<TodoItem> patchDoc)
    {
        if (patchDoc is null)
            return BadRequest();

        var dbTodoItem = await _todoService.GetById(id);
        if (dbTodoItem is null)
            return BadRequest();

        patchDoc.ApplyTo(dbTodoItem, ModelState);

        if (!ModelState.IsValid)
            return BadRequest();

        if (!TryValidateModel(dbTodoItem))
            return BadRequest();

        var todoItemUpdate = new TodoItemUpdate()
        {
            Description = dbTodoItem.Description,
            IsComplete = dbTodoItem.IsComplete
        };

        var updatedTodo = await _todoService.Update(id, todoItemUpdate);
        return Ok(updatedTodo);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteTodo(int id)
    {
        return await _todoService.Delete(id)
            ? NoContent()
            : NotFound();
    }

    [HttpGet]
    [Route("complete")]
    public async Task<ActionResult<List<TodoItem>>> GetCompleteTodos()
    {
        var todos = await _todoService.GetAll();
        var todosComplete = todos.Where(todo => todo.IsComplete == true);
        return Ok(todosComplete);
    }
}
