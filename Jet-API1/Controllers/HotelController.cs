using Jet_API1.Model;
using Jet_API1.Services.Interfaces;
using Jet_API1.ViewModel.Cityes;
using Jet_API1.ViewModel.Hotel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Jet_API1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelController : ControllerBase
{
    private readonly IHotelService _service;
    public HotelController(IHotelService service)
    {
        _service = service;
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        var data = _service.GetAll();
        if (data.StatusCode == Enum.StatusCode.Ok)
        {
            return Ok(data);
        }
        else
        {
            Log.Fatal("Tour Agency = {@data}", data);
            return BadRequest();
        }
    }
    [HttpPost]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Create(CreateHotelVM hotel)
    {
        var data = await _service.Create(hotel);
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
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Updata(int id, CreateHotelVM hotel)
    {
        var data = await _service.Update(id, hotel);
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
    [Authorize(Policy = "Admin")]
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
