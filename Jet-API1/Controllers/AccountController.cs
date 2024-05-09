using Jet_API1.ViewModel.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        return Ok(user);
    }
    //[HttpPost]
    //public async Task<IActionResult> LogIn(AccountVM account)
    //{
    //    var user = await _userManager.FindByNameAsync(account.UserName);
    //    if (user == null)
    //    {
    //        return BadRequest("User hasn't been found");
    //    }
    //    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, account.Password, isPersistent: false, lockoutOnFailure: false);
    //    if (!result.Succeeded)
    //    {
    //        return BadRequest("Wrong Password or UserName");
    //    }
    //    return Ok();
    //}
}
