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
    public TaskController(TaskService taskService)
    {
        TaskService = taskService;
    }
    
    
    [HttpGet("Test")]
    public IActionResult Test()
    {
        return  Ok("21");
    }

    
    [HttpPost("Create")]
    public async Task<IActionResult> Create(UserTaskView model)
    {
        try
        {
            await TaskService.Create(new UserTask(){Text = model.Text});
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("Read")]
    public async Task<IActionResult> Read()
    {
        return Ok(await TaskService.GetItems());
    }
    
    
    [HttpGet("GetNotification")]
    public async Task<IActionResult> GetNotification()
    {
        var list = (await TaskService.GetItems()).Where(n=>n.WasView==false);
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

    [HttpPost("Update")]
    public async Task<IActionResult> Update(UserTask model)
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