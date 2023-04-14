using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Server.Services;
using TaskManager.Shared;
using TaskManager.Shared.Models;

namespace TaskManager.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController: ControllerBase
{
    private TaskService TaskService { get; set; }
    private readonly UserManager<UserModel> _userManager;
    public TaskController(TaskService taskService,UserManager<UserModel> userManager)
    {
        TaskService = taskService;
        _userManager = userManager;
    }
    
    
    [HttpGet("Test")]
    public IActionResult Test()
    {
        return  Ok("21");
    }

    [Authorize]
    [HttpPost("Create")]
    public async Task<IActionResult> Create(UserTaskView model)
    {
        try
        {
            var fromUser = HttpContext.User.Identity!.Name;
            
            var user = _userManager.Users.FirstOrDefault(u => u.TableNumber == model.TableNumber);
            var fromTaskUser =await _userManager.FindByNameAsync(fromUser!);

            if (!await _userManager.IsInRoleAsync(fromTaskUser!, "Admin") || user is null)
                return BadRequest("Not allowed");
            
            await TaskService.Create(new UserTask(){Text = model.Text,ToUserId = user!.Id,FromUserId = fromTaskUser!.Id});
            return Ok();

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    [HttpGet("Read")]
    public async Task<IActionResult> Read()
    {
        var fromUser = HttpContext.User.Identity!.Name;
        var user =await _userManager.FindByNameAsync(fromUser!);

        if (await _userManager.IsInRoleAsync(user, "Admin") || await _userManager.IsInRoleAsync(user, "User"))
            return Ok(await TaskService.GetItemsById(user.Id));
        
        return BadRequest("Not Allowed");
       
    }
    
    [Authorize]
    [HttpGet("GetNotification")]
    public async Task<IActionResult> GetNotification()
    {
        var fromUser = HttpContext.User.Identity!.Name;
        var user =await _userManager.FindByNameAsync(fromUser!);
        
        var list = (await TaskService.GetItems()).Where(n=>n.WasView==false && n.ToUserId==user!.Id);
        return Ok(list);
    }
    
    
    [HttpDelete("Delete")]
    public IActionResult Delete(string id)
    {
        try
        {
            TaskService.Delete(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpPost("Update")]
    public async Task<IActionResult> Update(UpdateTask model)
    {
        try
        {
            await TaskService.Update(model);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
}