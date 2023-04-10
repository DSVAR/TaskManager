namespace TaskManager.Shared;

public class UserView
{
    public Guid Id { get; set; }
    public string? Text { get; set; }
    public bool IsComplete { get; set; }
}