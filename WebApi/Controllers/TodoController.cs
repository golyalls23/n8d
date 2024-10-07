using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class TodoController(ILogger<TodoController> logger, TodoDb context) : ControllerBase
    {
        private readonly ILogger<TodoController> _logger = logger;
        private readonly TodoDb _context = context;
        
        //private List<TodoItem> TodoItems =
        //[
        //    new TodoItem() { Id = 1, Description = "A", IsComplete = false },
        //    new TodoItem() { Id = 2, Description = "B", IsComplete = true },
        //    new TodoItem() { Id = 3, Description = "C", IsComplete = false },
        //];

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetAll()
        {
            _logger.LogInformation("Todo GetAll Requested");
            var todoItems = _context.TodoItems.ToList();
            return Ok(todoItems);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<TodoItem> GetById(int id)
        {
            _logger.LogInformation($"Todo GetById: {id}");
            var todoItem = _context.TodoItems.Find(id);
            if (todoItem is null)
                return NotFound();

            return Ok(todoItem);
        }

        [HttpPost]
        public ActionResult<TodoItem> Create(TodoItem todoItem)
        {
            var lastTodoItem = _context.TodoItems.LastOrDefault();
            var id = lastTodoItem?.Id + 1 ?? 1;
            var todoItemNew = new TodoItem() { Id = id, Description = todoItem.Description, IsComplete = todoItem.IsComplete };
            _context.TodoItems.Add(todoItemNew);
            _context.SaveChanges();
            return Created(); // "GetById", new { id, todoItem });
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<TodoItem> UpdateTodo(int id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
                return BadRequest();

            var dbTodoItem = _context.TodoItems.Find(id);
            if (dbTodoItem is null)
                return NotFound();

            dbTodoItem.Description = todoItem.Description;
            dbTodoItem.IsComplete = todoItem.IsComplete;
            _context.SaveChanges();
            return NoContent();

        }

        [HttpGet]
        [Route("complete")]
        public ActionResult<List<TodoItem>> GetCompleteTodos()
        {
            var todosComplete = _context.TodoItems.Where(todo => todo.IsComplete == true).ToList();
            return Ok(todosComplete);
        }
    }
}
