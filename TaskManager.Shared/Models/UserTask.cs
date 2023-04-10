namespace TaskManager.Shared.Models;

public class UserTask
{
    public Guid Id { get; set; }
    public string? Text { get; set; }
    public bool IsComplete { get; set; }
    public string? IdUser { get; set; }
}