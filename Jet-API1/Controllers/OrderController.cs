using Jet_API1.Model;
using Jet_API1.Services.Interfaces;
using Jet_API1.ViewModel.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Jet_API1.Controllers;

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
    [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
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
        [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(CreateOrderVM order)
        {
            var data = await _service.Create(order);
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
        [HttpDelete]
    [Authorize]
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
        [HttpPut]
    [Authorize]
    public async Task<IActionResult> Update(int id , CreateOrderVM order)
        {
            var data = await _service.Update(id , order);
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
    //[HttpGet]
    //[Route("page")]
    //public async Task<IActionResult> GetbyPage(int page)
    //{
    //    var data = _service.GetbyPage(page);
    //    if (data.StatusCode == Enum.StatusCode.Ok)
    //    {
    //        Log.Information("Tour Agency = {@data}", data);
    //        return Ok(data);
    //    }
    //    else
    //    {
    //        return BadRequest();
    //    }
    //}
}
