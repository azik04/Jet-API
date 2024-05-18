using Jet_API1.Services.Interfaces;
using Jet_API1.ViewModel.Regions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Jet_API1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegionController : ControllerBase
{
    private readonly IRegionService _service;
    public RegionController(IRegionService service)
    {
        _service = service;
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        var data = _service.GetAll();
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
    [HttpPost]
    [Authorize(Policy = "AdminOrSuperAdmin")]
    public async Task<IActionResult> Create(CreateRegionVM region)
    {
        var data = await _service.Create(region);
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
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await _service.Get(id);
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
    [HttpPut]
    [Authorize(Policy = "AdminOrSuperAdmin")]
    public async Task<IActionResult> Updata(int id, CreateRegionVM region)
    {
        var data = await _service.Update(id, region);
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
    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminOrSuperAdmin")]
    public async Task<IActionResult> Remove(int id)
    {
        var data = await _service.Delete(id);
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
