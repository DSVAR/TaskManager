using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.Shared.Models;

namespace TaskManager.Server.Entity;

public class ApplicationContext:IdentityDbContext<UserModel>
{
    private DbSet<UserTask>? UserTasks { get; set; }

    public ApplicationContext(DbContextOptions options):base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}