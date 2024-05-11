using Jet_API1.Model;
using Jet_API1.Services.Interfaces;
using Jet_API1.ViewModel.Orders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jet_API1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
            private readonly IOrderService _service;
            public OrderController(IOrderService service)
            {
                _service = service;
            }
            [HttpGet]
            public async Task<IActionResult> GetAll()
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
            [HttpGet("{id}")]
            public async Task<IActionResult> Get(int id)
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
            [HttpPost]
            public async Task<IActionResult> Create(CreateOrderVM order)
            {
                var data = await _service.Create(order);
                if (data.StatusCode == Enum.StatusCode.Ok)
                {
                    return Ok(data);
                }
                else
                {
                    return BadRequest();
                }
            }
            [HttpDelete]
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
            [HttpPut]
            public async Task<IActionResult> Update(Order order)
            {
                var data = await _service.Update(order);
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
