using Jet_API1.Model;
using Jet_API1.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> Create(Place city)
    {
        var data = await _service.Create(city);
        if (data.StatusCode == Enum.StatusCode.Ok)
        {
            return Ok(data);
        }
        else
        {
            return BadRequest();
        }
    }
    [HttpGet]
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
    public async Task<IActionResult> Updata(Place city)
    {
        var data = await _service.Update(city);
        if (data.StatusCode == Enum.StatusCode.Ok)
        {
            return Ok(data);
        }
        else
        {
            return BadRequest();
        }
    }
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
