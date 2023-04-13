using Microsoft.AspNetCore.Identity;

namespace TaskManager.Server;

public class StartWorker:IHostedService
{
    private readonly IServiceProvider _serviceProvider ;

    public StartWorker(IServiceProvider configuration)
    {
        _serviceProvider = configuration;
    }
    
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var boss = new IdentityUser() {Email = "Boss@mail.com",UserName = "Boss"};
        var user = new IdentityUser() {Email = "User@mail.com",UserName = "User"};
        
        var roleUser = new IdentityRole() { Name = "User" };
        var roleAdmin = new IdentityRole() { Name = "Admin" };
        
        using (var serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>()
                   .CreateScope())
        {
            UserManager<IdentityUser> userManager =
                serviceScope.ServiceProvider.GetService<UserManager<IdentityUser>>()!;
            var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>()!;
            
            
            var bossIdentity = await userManager!.FindByNameAsync("Boss");
            var userIdentity = await userManager!.FindByNameAsync("User");

            if (bossIdentity is null)
            {
                await userManager!.CreateAsync(boss,"BossPass_1");
                bossIdentity = await userManager!.FindByNameAsync("Boss");
            }

            if (userIdentity is null)
            {
                await userManager!.CreateAsync(user,"UserPass_1");
                userIdentity = await userManager!.FindByNameAsync("User");
            }
            
            if (await roleManager.FindByNameAsync(roleUser.Name) is null)
            {
                await roleManager.CreateAsync(roleUser);
            }
            if (await roleManager.FindByNameAsync(roleAdmin.Name) is null)
            {
                await roleManager.CreateAsync(roleAdmin);
            }
            
            
            if (!await userManager.IsInRoleAsync(bossIdentity!, roleUser.Name))
            {
                await userManager.AddToRoleAsync(bossIdentity!, roleUser.Name);
            }
            if (!await userManager.IsInRoleAsync(bossIdentity!, roleAdmin.Name))
            {
                await userManager.AddToRoleAsync(bossIdentity!, roleAdmin.Name);
            }
            if (!await userManager.IsInRoleAsync(userIdentity!, roleUser.Name))
            {
                await userManager.AddToRoleAsync(userIdentity!, roleUser.Name);
            }
            
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)=>Task.CompletedTask;
}