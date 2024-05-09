using Jet_API1.Model;
using Jet_API1.Services.Interfaces;
using Jet_API1.ViewModel.Hotel;
using Jet_API1.ViewModel.Region;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jet_API1.Controllers
{
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
                return Ok(data);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRegionVM region)
        {
            var data = await _service.Create(region);
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
        public async Task<IActionResult> Updata(Region region, int id)
        {
            var data = await _service.Update(region, id);
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
}
