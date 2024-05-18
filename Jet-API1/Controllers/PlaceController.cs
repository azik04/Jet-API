using Jet_API1.Services.Interfaces;
using Jet_API1.ViewModel.Places;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Jet_API1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlaceController : ControllerBase
{
    private readonly IPlaceService _service;
    public PlaceController(IPlaceService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var response = _service.GetAll();
        if (response.StatusCode == Enum.StatusCode.Ok)
        {
            return Ok(response);
        }
        else
        {
            Log.Fatal("Tour Agency = {@data}", response);
            return BadRequest(response);
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await _service.Get(id);
        if (response.StatusCode == Enum.StatusCode.Ok)
        {
            Log.Information("Tour Agency = {@response}", response);
            return Ok(response);
        }
        else
        {
            Log.Fatal("Tour Agency = {@data}", response);
            return BadRequest(response);
        }
    }
    [HttpPut]
    [Authorize(Policy = "AdminOrSuperAdmin")]
    public async Task<IActionResult> Update(int id, CreatePalaceVM place)
    {
        var response = await _service.Update(id, place);
        if (response.StatusCode == Enum.StatusCode.Ok)
        {
            Log.Information("Tour Agency = {@response}", response);
            return Ok(response);
        }
        else
        {
            Log.Fatal("Tour Agency = {@data}", response);
            return BadRequest(response);
        }
    }

    [HttpPost]
    [Authorize(Policy = "AdminOrSuperAdmin")]
    public async Task<IActionResult> Create(CreatePalaceVM place)
    {
        var response = await _service.Create(place);
        if (response.StatusCode == Enum.StatusCode.Ok)
        {
            Log.Information("Tour Agency = {@response}", response);
            return CreatedAtAction(nameof(GetAll), response);
        }
        else
        {
            Log.Fatal("Tour Agency = {@data}", response);
            return BadRequest(response);
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminOrSuperAdmin")]
    public async Task<IActionResult> Remove(int id)
    {
        var response = await _service.Delete(id);
        if (response.StatusCode == Enum.StatusCode.Ok)
        {
            Log.Information("Tour Agency = {@response}", response);
            return Ok(response);
        }
        else
        {
            Log.Fatal("Tour Agency = {@data}", response);
            return BadRequest(response);
        }
    }
}
