using Jet_API1.Model;
using Jet_API1.Services.Interfaces;
using Jet_API1.ViewModel.Flights;
using Jet_API1.ViewModel.Hotel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jet_API1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FlightController : ControllerBase
{
    private readonly IFlightService _service;
    public FlightController(IFlightService service)
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
            return BadRequest();
        }
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateFlightVM flight)
    {
        var data = await _service.Create(flight);
        if (data.StatusCode == Enum.StatusCode.Ok)
        {
            return Ok(data);
        }
        else
        {
            return BadRequest();
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await _service.Get(id);
        if (data.StatusCode == Enum.StatusCode.Ok)
        {
            return Ok(data);
        }
        else
        {
            return BadRequest();
        }
    }
    [HttpPut]
    public async Task<IActionResult> Updata(Flight flight, int id)
    {
        var data = await _service.Update(flight, id);
        if (data.StatusCode == Enum.StatusCode.Ok)
        {
            return Ok(data);
        }
        else
        {
            return BadRequest();
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        var data = await _service.Delete(id);
        if (data.StatusCode == Enum.StatusCode.Ok)
        {
            return Ok(data);
        }
        else
        {
            return BadRequest();
        }
    }
}
