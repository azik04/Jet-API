using Jet_API1.ViewModel.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jet_API1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }
    [HttpPost]
    public async Task<IActionResult> Register(AccountVM account)
    {

        IdentityUser user = new IdentityUser()
        {
            UserName = account.UserName,
        };
        await _userManager.CreateAsync(user, account.Password);
        var role = await _userManager.AddToRoleAsync(user, "User");
        if (role.Succeeded)
        {
            return Ok(user);
        }
        else
        {
            return BadRequest();
        }
    }
    [HttpPut("Admin")]
    public async Task<IActionResult> UpdateRole(string userName)
    {
        var user = _userManager.FindByNameAsync(userName);
        if(user == null)
        {
            return BadRequest();
        }
        var rem = await _userManager.RemoveFromRoleAsync(await user, "User");
        var role = await _userManager.AddToRoleAsync(await user, "Admin");
        if (role.Succeeded)
        {
            return Ok(user);
        }
        else
        {
            return BadRequest();
        }
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(AccountVM model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user == null)
        {
            return BadRequest("User not found");
        }
        var result = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            return Ok(model);
        }
        else
        {
            return Unauthorized("Invalid username or password");
        }
    }
    [HttpPost("Roles")]
    public async Task<IActionResult> AddRole()
    {
        IdentityRole role = new IdentityRole
        {
            Name = "User"
        };
        IdentityRole role1 = new IdentityRole
        {
            Name = "Admin"
        };
        IdentityRole role2 = new IdentityRole
        {
            Name = "SuperAdmin"
        };
        await _roleManager.CreateAsync(role);
        await _roleManager.CreateAsync(role1);
        await _roleManager.CreateAsync(role2);
        return Ok("Ok");
    }
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var data = await _userManager.Users.ToListAsync();
        if (data != null)
        {
            return Ok(data);
        }
        else
        {
            return BadRequest("Users Not Found");
        }
    }
    [HttpGet("GetRoles")]
    public async Task<IActionResult> GetAllRoles(string userName)
    {
        var data = await _userManager.FindByNameAsync(userName);
        if (data == null)
        {
            return NotFound($"User with ID {data} not found.");
        }

        var userRoles = await _userManager.GetRolesAsync(data);

        return Ok(userRoles);
    }
}





