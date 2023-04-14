using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TaskManager.Shared.Models;

public class UserTask
{
    public Guid Id { get; set; }
    public string? Text { get; set; }
    
    public bool IsComplete { get; set; }
    
    [ForeignKey("FromUserId")]
    public string? FromUserId { get; set; }
    public virtual UserModel? FromUserIdKey { get; set; }
    [ForeignKey("MainUserId")]
    public string? ToUserId { get; set; }
    public virtual UserModel? ToUserIdKey { get; set; }
    
   
    public bool WasView { get; set; }
}