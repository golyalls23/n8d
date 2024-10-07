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
        [HttpGet]
        [Route("complete")]
        public ActionResult<List<TodoItem>> GetCompleteTodos()
        {
            var todosComplete = _context.TodoItems.Where(todo => todo.IsComplete == true).ToList();
            return Ok(todosComplete);
        }
    }
}
