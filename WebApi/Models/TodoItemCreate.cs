namespace WebApi.Models;

public class TodoItemCreate
{
    public required string Description { get; set; }
    public bool IsComplete { get; set; }
}
