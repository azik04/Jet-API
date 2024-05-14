using Jet_API1.Services.Interfaces;
using Jet_API1.ViewModel.Account;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Jet_API1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IUserService _service;
    public AccountController(IUserService service)
    {
        _service = service;
    }
    [HttpPost("Register")]
    public async Task<IActionResult> Register(AccountVM account)
    {
        var data = await _service.Register(account);
        if(data.StatusCode == Enum.StatusCode.Ok)
        {

            Log.Information("Tour Agency = {@data}", data);
            return Ok(data);
        }
        else
        {
            return BadRequest();
        }
    }
    [HttpPost("LogIn")]
    public async Task<IActionResult> Login(AccountVM model)
    {
        var data = await _service.LogIn(model);
        if (data.StatusCode == Enum.StatusCode.Ok)
        {

            Log.Information("Tour Agency = {@data}", data);
            return Ok(data);
        }
        else
        {
            return BadRequest();
        }
    }
    [HttpPut("UpdRole")]
    public async Task<IActionResult> UpdateRole(string userName)
    {
        var data = await _service.UpdateRole(userName);
        if (data.StatusCode == Enum.StatusCode.Ok)
        {

            Log.Information("Tour Agency = {@data}", data);
            return Ok(data);
        }
        else
        {
            return BadRequest();
        }
    }
    [HttpPost("Roles")]
    public async Task<IActionResult> AddRole()
    {
        var data = await _service.CreateRole();
        if (data.StatusCode == Enum.StatusCode.Ok)
        {

            Log.Information("Tour Agency = {@data}", data);
            return Ok(data);
        }
        else
        {
            return BadRequest();
        }
    }
    [HttpGet("GetUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
        var data = await _service.GetAllUsers();
        if (data.StatusCode == Enum.StatusCode.Ok)
        {
            Log.Information("Tour Agency = {@data}", data);
            return Ok(data);
        }
        else
        {
            return BadRequest();
        }
    }
    [HttpGet("GetRoles")]
    public async Task<IActionResult> GetRole(string userName)
    {
        var data = await _service.GetRole(userName);
        if (data.StatusCode == Enum.StatusCode.Ok)
        { 
            Log.Information("Tour Agency = {@data}", data);
            return Ok(data);
        }
        else
        {
            return BadRequest();
        }
    }
}





