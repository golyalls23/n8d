namespace WebApi.Models;

public class TodoItemUpdate 
{
    public int Id { get; set; }
    public required string Description { get; set; }
    public bool IsComplete { get; set; }
}
