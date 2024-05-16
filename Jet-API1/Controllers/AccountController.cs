using Jet_API1.Services.Interfaces;
using Jet_API1.ViewModel.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
            Log.Fatal("Tour Agency = {@data}", data);
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
            Log.Fatal("Tour Agency = {@data}", data);
            return BadRequest();
        }
    }
    [HttpPut("CrtAdmin")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> CreateAdmin(string userName)
    {
        var data = await _service.UpdateRole(userName);
        if (data.StatusCode == Enum.StatusCode.Ok)
        {

            Log.Information("Tour Agency = {@data}", data);
            return Ok(data);
        }
        else
        {
            Log.Fatal("Tour Agency = {@data}", data);
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
            Log.Fatal("Tour Agency = {@data}", data);
            return BadRequest();
        }
    }
    [HttpGet("GetUsers")]
    [Authorize(Policy = "Admin")]
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
            Log.Fatal("Tour Agency = {@data}", data);
            return BadRequest();
        }
    }
    [HttpGet("GetRoles")]
    [Authorize(Policy = "Admin")]
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
            Log.Fatal("Tour Agency = {@data}", data);
            return BadRequest();
        }
    }
}





