using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class TodoController(ILogger<TodoController> logger, TodoDbContext context) : ControllerBase
    {
        private readonly ILogger<TodoController> _logger = logger;
        private readonly TodoDbContext _context = context;
        
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
            var todoItems = await _context.TodoItems.ToListAsync();
            return Ok(todoItems);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<TodoItem>> GetById(int id)
        {
            _logger.LogInformation("Todo GetById: {id}", id);
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem is null)
                return NotFound();

            return Ok(todoItem);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> Create(TodoItem todoItem)
        {
            var lastTodoItem = await _context.TodoItems.LastOrDefaultAsync();
            var id = lastTodoItem?.Id + 1 ?? 1;
            var todoItemNew = new TodoItem() { Id = id, Description = todoItem.Description, IsComplete = todoItem.IsComplete };
            await _context.TodoItems.AddAsync(todoItemNew);
            await _context.SaveChangesAsync();
            return Created(); // "GetById", new { id, todoItem });
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<TodoItem>> UpdateTodo(int id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
                return BadRequest();

            var dbTodoItem = await _context.TodoItems.FindAsync(id);
            if (dbTodoItem is null)
                return NotFound();

            dbTodoItem.Description = todoItem.Description;
            dbTodoItem.IsComplete = todoItem.IsComplete;
            await _context.SaveChangesAsync();
            return NoContent();

        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult> UpdateParitialTodo(int id, [FromBody] JsonPatchDocument<TodoItem> patchDoc)
        {
            if (patchDoc is null)
                return BadRequest();
            
            var todoItemFromDb = await _context.TodoItems.FindAsync(id);
            if (todoItemFromDb is null)
                return BadRequest();

            patchDoc.ApplyTo<TodoItem>(todoItemFromDb, ModelState);

            if (!ModelState.IsValid)
                return BadRequest();
            
            if (!TryValidateModel(todoItemFromDb))
                return BadRequest();

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteTodo(int id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem is null)
                return NotFound();

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet]
        [Route("complete")]
        public async Task<ActionResult<List<TodoItem>>> GetCompleteTodos()
        {
            var todosComplete = await _context.TodoItems.Where(todo => todo.IsComplete == true).ToListAsync();
            return Ok(todosComplete);
        }
    }
}
