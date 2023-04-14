using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Server.Entity;
using TaskManager.Shared.Models;
using TaskManager.Shared.Models.ViewModels;

namespace TaskManager.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController: ControllerBase
{
    private UserManager<UserModel> _userManager { get; set; }

    public AuthController(UserManager<UserModel> userManager)
    {
        _userManager = userManager;
    }

    
    
    [HttpGet("Enter")]
    public async Task<IActionResult> Auth()
    {
        //  var s=UserManager.GetClaimsAsync()

        try
        {
            var user = await _userManager.FindByNameAsync("Boss");
            var result = await _userManager.CheckPasswordAsync(user!, "BossPass_1");

            if (result)
            {
                var roles = await _userManager.GetRolesAsync(user);
                List<Claim> claims = new List<Claim>()
                {
                    new (ClaimTypes.Name, user!.UserName!),
                };
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var now = DateTime.Now;
                var jwt = new JwtSecurityToken(
                    issuer: AutOptions.ISSUER,
                    audience: AutOptions.AUDIENCE,
                    notBefore: now,
                    claims: claims,
                    expires: now.Add(TimeSpan.FromHours(AutOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(
                        AutOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodeJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                
                return Ok(encodeJwt);
            }
        }
        catch (Exception ex)
        {
            return Ok(ex.Message);
        }

        return Ok();
    }
    
    [HttpPost("Enter")]
    public async Task<IActionResult> AuthPost(EnterView model)
    {
        //  var s=UserManager.GetClaimsAsync()

        try
        {
            var user = await _userManager.FindByNameAsync(model.Login!);
            var result = await _userManager.CheckPasswordAsync(user!, model.Password!);

            if (result)
            {
                var roles = await _userManager.GetRolesAsync(user!);
                List<Claim> claims = new List<Claim>()
                {
                    new (ClaimTypes.Name, user!.UserName!),
                };
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var now = DateTime.Now;
                var jwt = new JwtSecurityToken(
                    issuer: AutOptions.ISSUER,
                    audience: AutOptions.AUDIENCE,
                    notBefore: now,
                    claims: claims,
                    expires: now.Add(TimeSpan.FromHours(AutOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(
                        AutOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodeJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                
                return Ok(encodeJwt);
            }
            else
            {
                return BadRequest("Login or password incorrect");
            }
        }
        catch (Exception ex)
        {
            return Ok(ex.Message);
        }

    }

   
    [HttpGet("Test")]
    [Authorize]
    public async Task<IActionResult> Test()
    {
        var user = HttpContext.User.Identity!.Name;
        var foundUser =await _userManager.FindByNameAsync(user);
        return Ok("great");
    }
}