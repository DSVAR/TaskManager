using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Shared.Models;
using TaskManager.Shared.Models.ViewModels;

namespace TaskManager.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController: ControllerBase
{
    private readonly UserManager<UserModel> _userManager;

    public UserController(UserManager<UserModel> userManager)
    {
        _userManager = userManager;
    }
    
    [Authorize]
    [HttpGet("GetUsersTableNumber")]
    public async Task<IActionResult> GetUsersTableNumber()
    {
        
        var userName = HttpContext.User.Identity!.Name;
        var user = await _userManager.FindByNameAsync(userName!);
        if (await _userManager.IsInRoleAsync(user,"Admin"))
        {
            var listItemPicker = new List<ItemPicker>();
            var listUser = _userManager.Users.ToList();
            foreach (var userL in listUser)
            {
                listItemPicker.Add(new ItemPicker(){Id = userL.TableNumber,Name = userL.UserName});
            }
            return Ok(listItemPicker);
        }
        else
        {
            var userObj = _userManager.Users.FirstOrDefault(x=>x.TableNumber==user!.TableNumber);
            return Ok(new ItemPicker(){Id = userObj.TableNumber,Name = userObj.UserName});
        }

        return BadRequest("not have role");
    }

}