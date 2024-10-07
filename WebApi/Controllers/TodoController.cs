using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class TodoController(ILogger<TodoController> logger) : ControllerBase
    {
        private readonly ILogger<TodoController> _logger = logger;
    }
}
