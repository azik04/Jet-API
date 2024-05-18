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
    [HttpPut("UpdateRoles")]
    [Authorize(Policy = "SuperAdmin")]
    public async Task<IActionResult> UpdateRole(string userName, string userRole)
    {
        var data = await _service.UpdateRole(userName, userRole);
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
    [HttpPost("CreateRoles")]
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
    [HttpGet("GetAllUsers")]
    [Authorize(Policy = "AdminOrSuperAdmin")]
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
    [HttpGet("GetUser")]
    [Authorize(Policy = "AdminOrSuperAdmin")]
    public async Task<IActionResult> GetUser(string userName)
    {
        var data = await _service.GetUser(userName);
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
    [HttpGet("GetUsersByRoles")]
    [Authorize(Policy = "AdminOrSuperAdmin")]
    public async Task<IActionResult> GetUsersByRoles(string roleName)
    {
        var data = await _service.GetAllUserByRoles(roleName);
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





