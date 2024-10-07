using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class TodoController(ILogger<TodoController> logger) : ControllerBase
    {
        private readonly ILogger<TodoController> _logger = logger;

        private List<TodoItem> TodoItems =
        [
            new TodoItem() { Id = 1, Description = "A", IsComplete = false },
            new TodoItem() { Id = 2, Description = "B", IsComplete = true },
            new TodoItem() { Id = 3, Description = "C", IsComplete = false },
        ];

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetAll()
        {
            _logger.LogInformation("Todo GetAll Requested");
            return Ok(TodoItems);
        }
    }
}
