using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TaskManager.Shared.Models;

public class UserModel:IdentityUser
{
    public int TableNumber { get; set; }
    
 
}