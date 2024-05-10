using Jet_API1.Model;
using Jet_API1.Services.Interfaces;
using Jet_API1.ViewModel.Places;
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
        var response = _service.GetAll();
        if (response.StatusCode == Enum.StatusCode.Ok)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest(response);
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await _service.Get(id);
        if (response.StatusCode == Enum.StatusCode.Ok)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest(response);
        }
    }
    [HttpPut]
    public async Task<IActionResult> Update(Place place)
    {
        var response = await _service.Update(place);
        if (response.StatusCode == Enum.StatusCode.Ok)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest(response);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePalaceVM place)
    {
        var response = await _service.Create(place);
        if (response.StatusCode == Enum.StatusCode.Ok)
        {
            return CreatedAtAction(nameof(GetAll), response);
        }
        else
        {
            return BadRequest(response);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        var response = await _service.Delete(id);
        if (response.StatusCode == Enum.StatusCode.Ok)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest(response);
        }
    }
}
