namespace TaskManager.Shared.Models.ViewModels;

public class NotificationViewModel
{
    public Guid Id { get; set; }
    public string? Text { get; set; }
    public string? FromUserId { get; set; }
}